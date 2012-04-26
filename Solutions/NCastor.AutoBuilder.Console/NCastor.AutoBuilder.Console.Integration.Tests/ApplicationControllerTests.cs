using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCastor.AutoBuilder.Console.Constants;
using NCastor.AutoBuilder.Console.FluentConfiguration;
using Microsoft.Practices.ServiceLocation;

namespace NCastor.AutoBuilder.Console.Integration.Tests
{
    [TestClass]
    public class ApplicationControllerTests
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            new BootstrapperInitialization().Start();
        }

        [TestMethod]
        public void can_create_new_ApplicationController()
        {
            var cont = new ApplicationControllerBuilder().BuildWithNulls();

            cont.Should().NotBeNull();
        }

        [TestClass]
        public class TheAreArgumentsValidMethod
        {
            [TestMethod]
            public void when_the_arguments_specified_are_null_it_should_throw_an_ArgumentException()
            {
                var cont = new ApplicationControllerBuilder().BuildWithNulls();

                cont.Invoking(x => x.AreArgumentsValid())
                    .ShouldThrow<ArgumentNullException>()
                    .WithMessage("The arguments specified must not be null", FluentAssertions.Assertions.ComparisonMode.Substring);
            }

            [TestMethod]
            public void when_the_arguments_are_invalid_it_should_return_false()
            {
                var cont = GetApplicationController("-p");

                cont.AreArgumentsValid().Should().BeFalse();
            }

            [TestMethod]
            public void when_calling_with_valid_arguments_it_should_return_true()
            {
                var cont = GetApplicationController("-p", "my app", "-o", ".");

                cont.AreArgumentsValid().Should().BeTrue();
            }
        }

        [TestClass]
        public class TheGetCommandHelpMethod
        {
            [TestMethod]
            public void it_should_return_the_help_specifying_how_to_use_the_application()
            {
                var cont = GetApplicationController("-p");

                cont.GetCommandLineHelp().Should().NotBeNullOrEmpty().And.NotBeBlank();
            }
        }

        [TestClass]
        public class TheProcessTemplateMethod
        {
            [TestMethod]
            public void calling_with_a_null_resource_name_argument_it_should_throw_an_ArgumentException()
            {
                var cont = new ApplicationControllerBuilder().BuildWithNulls();

                cont.Invoking(x => x.ProcessTemplate(null, null, null))
                    .ShouldThrow<ArgumentException>()
                    .WithMessage("embeddedResourceName", FluentAssertions.Assertions.ComparisonMode.Substring);
            }

            [TestMethod]
            public void calling_with_a_null_delegate_body_it_should_throw_an_ArgumentNullException()
            {
                var cont = new ApplicationControllerBuilder().BuildWithNulls();

                cont.Invoking(x => x.ProcessTemplate("ded", null, null))
                    .ShouldThrow<ArgumentNullException>()
                    .WithMessage("processingTemplate", FluentAssertions.Assertions.ComparisonMode.Substring);
            }

            [TestMethod]
            public void calling_with_invalid_argument_line_options_it_should_throw_an_ArgumentException()
            {
                var cont = GetApplicationController("-p", "My App", "-o");

                cont.Invoking(x => x.ProcessTemplate("my res", "my namespace", (w, y, z) => { }))
                    .ShouldThrow<ArgumentException>()
                    .WithMessage("The command line arguments must be valid before calling the ProcessTemplate method", FluentAssertions.Assertions.ComparisonMode.Substring);
            }
        }

        [TestClass]
        public class TheProcessSolutionTemplateMethod
        {
            [TestMethod]
            public void it_should_save_the_Solution_template()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".");

                var config = cont.ProcessSolutionTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();
                config.Persistence.OutputTemplatePath.Should().Be(@".\My App.BuildSolution.proj");

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);

                templateRes.CountOcurrences(config.Context.CurrentOptions.ProductName).Should().Be(2);
            }
        }

        [TestClass]
        public class TheProcessPropertiesCustomPropertiesTemplateMethod
        {
            [TestMethod]
            public void it_should_save_the_CustomProperties_template()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".");

                var config = cont.ProcessPropertiesCustomPropertiesTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();
                config.Persistence.OutputTemplatePath.Should().Be(@".\Properties\My App.Properties.import");

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);
            }
        }

        [TestClass]
        public class TheProcessPropertiesInitPropertiesTemplateMethod
        {
            [TestMethod]
            public void it_should_save_the_InitProperties_template()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".", "-s", this.GetType().Assembly.Location);
                var config = cont.ProcessPropertiesInitPropertiesTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();
                config.Persistence.OutputTemplatePath.Should().Be(@".\Properties\My App.InitProperties.import");

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);

                templateRes.CountOcurrences(config.Context.CurrentOptions.ProductName).Should().Be(2);
                templateRes.CountOcurrences(config.Context.CurrentOptions.SolutionName).Should().Be(1);

                templateRes.Should().NotContain("{{ SolutionName }}");
            }
        }

        [TestClass]
        public class TheProcessTasksCustomTasksTemplateMethod
        {
            [TestMethod]
            public void it_should_save_the_CustomTasks_template()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".");
                var config = cont.ProcessTasksCustomTasksTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();
                config.Persistence.OutputTemplatePath.Should().Be(@".\Tasks\My App.Tasks.import");

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);
            }
        }

        [TestClass]
        public class TheProcessTargetsCustomTargetsTemplateMethod
        {
            [TestMethod]
            public void it_should_save_the_CustomTargets_template()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".");
                var config = cont.ProcessTargetsCustomTargetsTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();
                config.Persistence.OutputTemplatePath.Should().Be(@".\Targets\My App.Targets.import");

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);

                templateRes.CountOcurrences(config.Context.CurrentOptions.ProductName).Should().Be(4);
            }
        }

        [TestClass]
        public class TheProcessTargetsBuildTargetsTemplateMethod
        {
            [TestMethod]
            public void it_should_save_the_BuildTargets_template()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".");
                var config = cont.ProcessTargetsBuildTargetsTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();
                config.Persistence.OutputTemplatePath.Should().Be(@".\Targets\Build\My App.BuildTargets.import");

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);
            }
        }

        [TestClass]
        public class TheProcessTargetsRunTestsTargetsTemplateMethod
        {
            [TestMethod]
            public void it_should_save_the_RunTestsTargets_template()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".");
                var config = cont.ProcessTargetsRunTestsTargetsTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);

                config.Persistence.OutputTemplatePath.Should().Be(@".\Targets\Tests\My App.RunTestsTargets.import");
            }
        }

        [TestClass]
        public class TheProcessTargetsVersioningTargetsTemplateMethod
        {
            [TestMethod]
            public void it_should_save_the_VersioningTargets_template()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".");
                var config = cont.ProcessTargetsVersioningTargetsTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);

                config.Persistence.OutputTemplatePath.Should().Be(@".\Targets\Build\My App.VersioningTargets.import");
            }
        }

        [TestClass]
        public class TheProcessTargetsCustomBuildTargetsTemplateMethod
        {
            [TestMethod]
            public void it_should_save_the_CustomBuildTargets_template()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".");
                var config = cont.ProcessTargetsCustomBuildTargetsTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);

                config.Persistence.OutputTemplatePath.Should().Be(@".\Targets\Build\My App.CustomBuildTargets.import");
            }
        }

        [TestClass]
        public class TheProcessCustomSolutionPropertiesTemplateMethod
        {
            [TestMethod]
            public void it_should_save_the_CustomSolutionProperties_template()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".");
                var config = cont.ProcessCustomSolutionPropertiesTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);

                config.Persistence.OutputTemplatePath.Should().Be(@".\My App.CustomSolution.properties");
                templateRes.CountOcurrences(config.Context.CurrentOptions.ProductName).Should().Be(2);
            }
        }

        [TestClass]
        public class TheProcessCustomSolutionTargetsTemplateMethod
        {
            [TestMethod]
            public void it_should_save_the_CustomsolutionTargets_template()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".");
                var config = cont.ProcessCustomSolutionTargetsTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);

                config.Persistence.OutputTemplatePath.Should().Be(@".\My App.CustomSolution.targets");
                templateRes.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreFormatSemanticVersion")
                    .And.Contain("CoreFormatFileVersion")
                    .And.Contain("CoreFormatInformationalVersion")
                    .And.Contain("<!--**********Common versioning targets-->")
                    .And.Contain("<!--**********End common versioning targets-->");
            }

            [TestMethod]
            public void when_not_specifying_a_CIS_it_should_save_the_CustomsolutionTargets_template_with_the_default_targets_code()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".");
                var config = cont.ProcessCustomSolutionTargetsTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);

                config.Persistence.OutputTemplatePath.Should().Be(@".\My App.CustomSolution.targets");
                templateRes.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreGetBuildVersion");

                templateRes.CountOcurrences("CoreGetBuildVersion").Should().Be(2);
            }

            [TestMethod]
            public void when_specifying_Hudson_as_the_current_CIS_it_should_save_the_CustomsolutionTargets_template_with_the_Hudson_targets_code()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".", "--cis", "hudson");
                var config = cont.ProcessCustomSolutionTargetsTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);

                config.Persistence.OutputTemplatePath.Should().Be(@".\My App.CustomSolution.targets");
                templateRes.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreGetBuildVersion")
                    .And.Contain("GetBuildVersionFromHudson");
                
                templateRes.CountOcurrences("CoreGetBuildVersion").Should().Be(1);
                templateRes.CountOcurrences("GetBuildVersionFromHudson").Should().Be(1);
            }

            [TestMethod]
            public void when_specifying_TeamCity_as_the_current_CIS_it_should_save_the_CustomsolutionTargets_template_with_the_TeamCity_targets_code()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".", "--cis", "teamcity");
                var config = cont.ProcessCustomSolutionTargetsTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);

                config.Persistence.OutputTemplatePath.Should().Be(@".\My App.CustomSolution.targets");
                templateRes.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreGetBuildVersion")
                    .And.Contain("GetBuildVersionFromTeamCity");

                templateRes.CountOcurrences("CoreGetBuildVersion").Should().Be(1);
                templateRes.CountOcurrences("GetBuildVersionFromTeamCity").Should().Be(1);
            }

            [TestMethod]
            public void when_specifying_CCNET_as_the_current_CIS_it_should_save_the_CustomsolutionTargets_template_with_the_CCNET_targets_code()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".", "--cis", "ccnet");
                var config = cont.ProcessCustomSolutionTargetsTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);

                config.Persistence.OutputTemplatePath.Should().Be(@".\My App.CustomSolution.targets");
                templateRes.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreGetBuildVersion")
                    .And.Contain("GetBuildVersionFromCCNET");

                templateRes.CountOcurrences("CoreGetBuildVersion").Should().Be(1);
                templateRes.CountOcurrences("GetBuildVersionFromCCNET").Should().Be(1);
            }

            [TestMethod]
            public void when_specifying_TFS_as_the_current_CIS_it_should_save_the_CustomsolutionTargets_template_with_the_TFS_targets_code()
            {
                var cont = GetApplicationController("-p", "My App", "-o", ".", "--cis", "tfs");
                var config = cont.ProcessCustomSolutionTargetsTemplate();

                config.Should().NotBeNull();
                File.Exists(config.Persistence.OutputTemplatePath).Should().BeTrue();

                //testing that the template tokens were substituted correctly
                var templateRes = GetContentFromPersistedTemplate(config);

                config.Persistence.OutputTemplatePath.Should().Be(@".\My App.CustomSolution.targets");
                templateRes.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreGetBuildVersion")
                    .And.Contain("GetBuildVersionFromTFS");

                templateRes.CountOcurrences("CoreGetBuildVersion").Should().Be(1);
                templateRes.CountOcurrences("GetBuildVersionFromTFS").Should().Be(1);
            }
        }

        private static ApplicationController GetApplicationController(params string[] arguments)
        {
            var argumentsValidator = ServiceLocator.Current.GetInstance<ArgumentsValidator>();
            argumentsValidator.AreArgumentsValid(arguments);

            return ServiceLocator.Current.GetInstance<ApplicationController>().WithArguments(arguments);
        }

        private static string GetContentFromPersistedTemplate(TemplateConfigurator config)
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
