using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using NCastor.Console.Constants;
using System.IO;
using NCastor.Console.FluentConfiguration;

namespace NCastor.Console.Integration.Tests
{
    [TestClass]
    public class ApplicationControllerTests
    {
        [TestMethod]
        public void can_create_new_ApplicationController()
        {
            var cont = new ApplicationController(null);

            cont.Should().NotBeNull();
        }

        #region Tests for the AreArgumentsValid method

        [TestMethod]
        public void calling_AreArgumentsValid_when_the_arguments_specified_are_null_it_should_throw_an_ArgumentException()
        {
            var cont = new ApplicationController(null);

            cont.Invoking(x => x.AreArgumentsValid())
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("The arguments specified must not be null", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void when_calling_AreArgumentsValid_with_invalid_arguments_it_should_return_false()
        {
            var args = new[] { "-p" };
            var cont = new ApplicationController(args);

            cont.AreArgumentsValid().Should().BeFalse();
        }

        [TestMethod]
        public void when_calling_AreArgumentsValid_with_valid_arguments_it_should_return_true()
        {
            var args = new[] { "-p", "my app", "-o", "." };
            var cont = new ApplicationController(args);

            cont.AreArgumentsValid().Should().BeTrue();
        }

        #endregion

        #region Tests for the GetCommandHelp method

        [TestMethod]
        public void when_calling_GetCommandHelp_it_should_return_the_help_specifying_how_to_use_the_application()
        {
            var cont = new ApplicationController(new[] { "-p" });

            cont.GetCommandLineHelp().Should().NotBeNullOrEmpty().And.NotBeBlank();
        }

        #endregion

        #region tests for the method ProcessTemplate

        [TestMethod]
        public void calling_ProcessTemplate_with_a_null_resource_name_argument_it_should_throw_an_ArgumentException()
        {
            var cont = new ApplicationController(null);

            cont.Invoking(x => x.ProcessTemplate(null, null, null))
                .ShouldThrow<ArgumentException>()
                .WithMessage("embeddedResourceName", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void calling_ProcessTemplate_with_a_null_delegate_body_it_should_throw_an_ArgumentNullException()
        {
            var cont = new ApplicationController(null);

            cont.Invoking(x => x.ProcessTemplate("ded", null, null))
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("processingTemplate", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void calling_ProcessTemplate_with_invalid_argument_line_options_it_should_throw_an_ArgumentException()
        {
            var cont = new ApplicationController(new[] { "-p", "My App", "-o" });

            cont.Invoking(x => x.ProcessTemplate("my res", "my namespace", (w, y, z) => { }))
                .ShouldThrow<ArgumentException>()
                .WithMessage("The command line arguments must be valid before calling the ProcessTemplate method", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void calling_Processtemplate_for_the_Solution_template_it_should_save_the_template()
        {
            var cont = new ApplicationController(new[] { "-p", "My App", "-o", "." });

            var config = cont.ProcessTemplate(TemplateConstants.Solution, "NCastor.Console.Templates", (x, y, z) =>
            {
                x.Set(TemplateTokenConstants.ProductName, y.ProductName);
            });

            config.Should().NotBeNull();
            config.Processor.FinalTemplateFileName.Should().NotBeNullOrEmpty().And.NotBeBlank();
            File.Exists(config.Processor.FinalTemplateFileName);
        }

        #endregion

        [TestMethod]
        public void calling_ProcessSolutionTemplate_should_save_the_Solution_template()
        {
            var cont = new ApplicationController(new[] { "-p", "My App", "-o", "." });

            var config = cont.ProcessSolutionTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);

            templateRes.CountOcurrences(config.Context.CurrentOptions.ProductName).Should().Be(9);
            templateRes.CountOcurrences(config.Processor.FinalTemplateFileName).Should().Be(1);
        }

        [TestMethod]
        public void calling_ProcessPropertiesCustomPropertiesTemplate_should_save_the_template()
        {
            var cont = new ApplicationController(new[] { "-p", "My App", "-o", "." });

            var config = cont.ProcessPropertiesCustomPropertiesTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);

            templateRes.CountOcurrences(config.Processor.FinalTemplateFileName).Should().Be(1);
        }

        [TestMethod]
        public void calling_ProcessPropertiesInitPropertiesTemplate_should_save_the_template()
        {
            var cont = new ApplicationController(new[] { "-p", "My App", "-o", ".", "-s", this.GetType().Assembly.Location });

            var config = cont.ProcessPropertiesInitPropertiesTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);

            templateRes.CountOcurrences(config.Context.CurrentOptions.ProductName).Should().Be(3);
            templateRes.CountOcurrences(config.Processor.FinalTemplateFileName).Should().Be(1);
            templateRes.CountOcurrences(config.Context.CurrentOptions.SolutionName).Should().Be(1);

            templateRes.Should().NotContain("{{ SolutionName }}");
        }

        private string GetContentFromPersistedTemplate(TemplateConfigurator config)
        {
            var templateRes = string.Empty;

            using (var reader = new StreamReader(config.Persistence.OutputTemplatePath))
            {
                templateRes = reader.ReadToEnd();
            }

            templateRes.Should().NotBeNullOrEmpty().And.NotBeBlank();

            return templateRes;
        }
    }
}
