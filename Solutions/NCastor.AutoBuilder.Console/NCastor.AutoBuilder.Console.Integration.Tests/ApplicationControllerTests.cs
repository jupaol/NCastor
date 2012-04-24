﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCastor.AutoBuilder.Console.Constants;
using NCastor.AutoBuilder.Console.FluentConfiguration;

namespace NCastor.AutoBuilder.Console.Integration.Tests
{
    [TestClass]
    public class ApplicationControllerTests
    {
        [TestMethod]
        public void can_create_new_ApplicationController()
        {
            var cont = new ApplicationControllerBuilder().BuildWithNulls();

            cont.Should().NotBeNull();
        }

        #region Tests for the AreArgumentsValid method

        [TestMethod]
        public void calling_AreArgumentsValid_when_the_arguments_specified_are_null_it_should_throw_an_ArgumentException()
        {
            var cont = new ApplicationControllerBuilder().BuildWithNulls();

            cont.Invoking(x => x.AreArgumentsValid())
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("The arguments specified must not be null", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void when_calling_AreArgumentsValid_with_invalid_arguments_it_should_return_false()
        {
            var args = new[] { "-p" };
            var cont = new ApplicationControllerBuilder().WithArguments(args).Build();

            cont.AreArgumentsValid().Should().BeFalse();
        }

        [TestMethod]
        public void when_calling_AreArgumentsValid_with_valid_arguments_it_should_return_true()
        {
            var args = new[] { "-p", "my app", "-o", "." };
            var cont = new ApplicationControllerBuilder().WithArguments(args).Build();

            cont.AreArgumentsValid().Should().BeTrue();
        }

        #endregion

        #region Tests for the GetCommandHelp method

        [TestMethod]
        public void when_calling_GetCommandHelp_it_should_return_the_help_specifying_how_to_use_the_application()
        {
            var cont = new ApplicationControllerBuilder().WithArguments(new[] { "-p" }).Build();

            cont.GetCommandLineHelp().Should().NotBeNullOrEmpty().And.NotBeBlank();
        }

        #endregion

        #region tests for the method ProcessTemplate

        [TestMethod]
        public void calling_ProcessTemplate_with_a_null_resource_name_argument_it_should_throw_an_ArgumentException()
        {
            var cont = new ApplicationControllerBuilder().BuildWithNulls();

            cont.Invoking(x => x.ProcessTemplate(null, null, null))
                .ShouldThrow<ArgumentException>()
                .WithMessage("embeddedResourceName", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void calling_ProcessTemplate_with_a_null_delegate_body_it_should_throw_an_ArgumentNullException()
        {
            var cont = new ApplicationControllerBuilder().BuildWithNulls();

            cont.Invoking(x => x.ProcessTemplate("ded", null, null))
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("processingTemplate", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void calling_ProcessTemplate_with_invalid_argument_line_options_it_should_throw_an_ArgumentException()
        {
            var cont = new ApplicationControllerBuilder().WithArguments(new[] { "-p", "My App", "-o" }).Build();

            cont.Invoking(x => x.ProcessTemplate("my res", "my namespace", (w, y, z) => { }))
                .ShouldThrow<ArgumentException>()
                .WithMessage("The command line arguments must be valid before calling the ProcessTemplate method", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        #endregion

        [TestMethod]
        public void calling_ProcessSolutionTemplate_should_save_the_Solution_template()
        {
            var cont = new ApplicationControllerBuilder().WithArguments(new[] { "-p", "My App", "-o", "." }).Build();

            var config = cont.ProcessSolutionTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();
            config.Persistence.OutputTemplatePath.Should().Be(@".\My App.BuildSolution.proj");

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);

            templateRes.CountOcurrences(config.Context.CurrentOptions.ProductName).Should().Be(2);
        }

        [TestMethod]
        public void calling_ProcessPropertiesCustomPropertiesTemplate_should_save_the_template()
        {
            var cont = new ApplicationControllerBuilder().WithArguments(new[] { "-p", "My App", "-o", "." }).Build();

            var config = cont.ProcessPropertiesCustomPropertiesTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();
            config.Persistence.OutputTemplatePath.Should().Be(@".\Properties\My App.Properties.import");

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);
        }

        [TestMethod]
        public void calling_ProcessPropertiesInitPropertiesTemplate_should_save_the_template()
        {
            var cont = new ApplicationControllerBuilder().WithArguments(new[] { "-p", "My App", "-o", ".", "-s", this.GetType().Assembly.Location }).Build();

            var config = cont.ProcessPropertiesInitPropertiesTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();
            config.Persistence.OutputTemplatePath.Should().Be(@".\Properties\My App.InitProperties.import");

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);

            templateRes.CountOcurrences(config.Context.CurrentOptions.ProductName).Should().Be(2);
            templateRes.CountOcurrences(config.Context.CurrentOptions.SolutionName).Should().Be(1);

            templateRes.Should().NotContain("{{ SolutionName }}");
        }

        [TestMethod]
        public void calling_ProcessTasksCustomTasksTemplate_should_save_the_template()
        {
            var cont = new ApplicationControllerBuilder().WithArguments(new[] { "-p", "My App", "-o", "." }).Build();

            var config = cont.ProcessTasksCustomTasksTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();
            config.Persistence.OutputTemplatePath.Should().Be(@".\Tasks\My App.Tasks.import");

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);
        }

        [TestMethod]
        public void calling_ProcessTargetsCustomTargetsTemplate_should_save_the_template_on_disk()
        {
            var cont = new ApplicationControllerBuilder().WithArguments(new[] { "-p", "My App", "-o", "." }).Build();

            var config = cont.ProcessTargetsCustomTargetsTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();
            config.Persistence.OutputTemplatePath.Should().Be(@".\Targets\My App.Targets.import");

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);

            templateRes.CountOcurrences(config.Context.CurrentOptions.ProductName).Should().Be(4);
        }

        [TestMethod]
        public void calling_ProcessTargetsBuildTargetsTemplate_should_save_the_template_on_disk()
        {
            var cont = new ApplicationControllerBuilder().WithArguments(new[] { "-p", "My App", "-o", "." }).Build();

            var config = cont.ProcessTargetsBuildTargetsTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();
            config.Persistence.OutputTemplatePath.Should().Be(@".\Targets\Build\My App.BuildTargets.import");

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);
        }

        [TestMethod]
        public void calling_ProcessTargetsRunTestsTargetsTemplate_should_save_the_template_on_disk()
        {
            var cont = new ApplicationControllerBuilder().WithArguments(new[] { "-p", "My App", "-o", "." }).Build();

            var config = cont.ProcessTargetsRunTestsTargetsTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);

            config.Persistence.OutputTemplatePath.Should().Be(@".\Targets\Tests\My App.RunTestsTargets.import");
        }

        [TestMethod]
        public void calling_ProcessTargetsVersioningTargetsTemplate_should_save_the_template_on_disk()
        {
            var cont = new ApplicationControllerBuilder().WithArguments(new[] { "-p", "My App", "-o", "." }).Build();

            var config = cont.ProcessTargetsVersioningTargetsTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);

            config.Persistence.OutputTemplatePath.Should().Be(@".\Targets\Build\My App.VersioningTargets.import");
        }

        [TestMethod]
        public void calling_ProcessTargetsCustomBuildTargetsTemplate_should_save_the_template_on_disk()
        {
            var cont = new ApplicationControllerBuilder().WithArguments(new[] { "-p", "My App", "-o", "." }).Build();

            var config = cont.ProcessTargetsCustomBuildTargetsTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);

            config.Persistence.OutputTemplatePath.Should().Be(@".\Targets\Build\My App.CustomBuildTargets.import");
        }

        [TestMethod]
        public void calling_ProcessCustomSolutionPropertiesTemplate_should_save_the_template_on_disk()
        {
            var cont = new ApplicationControllerBuilder().WithArguments(new[] { "-p", "My App", "-o", "." }).Build();

            var config = cont.ProcessCustomSolutionPropertiesTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);

            config.Persistence.OutputTemplatePath.Should().Be(@".\My App.CustomSolution.properties");
            templateRes.CountOcurrences(config.Context.CurrentOptions.ProductName).Should().Be(2);
        }

        [TestMethod]
        public void calling_ProcessCustomSolutionTargetsTemplate_should_save_the_template_on_disk()
        {
            var cont = new ApplicationControllerBuilder().WithArguments(new[] { "-p", "My App", "-o", "." }).Build();

            var config = cont.ProcessCustomSolutionTargetsTemplate();

            config.Should().NotBeNull();
            File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

            //testing that the template tokens were substituted correctly
            var templateRes = this.GetContentFromPersistedTemplate(config);

            config.Persistence.OutputTemplatePath.Should().Be(@".\My App.CustomSolution.targets");
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
