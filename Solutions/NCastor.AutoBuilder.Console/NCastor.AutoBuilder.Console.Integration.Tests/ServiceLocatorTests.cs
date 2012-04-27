using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.ServiceLocation;
using FluentAssertions;
using CommandLine;
using NCastor.AutoBuilder.Console.CodeGenerator.Targets.Build.Versioning;
using NCastor.AutoBuilder.Console.CodeGenerator.Properties.Runners;

namespace NCastor.AutoBuilder.Console.Integration.Tests
{
    [TestClass]
    public class ServiceLocatorTests
    {
        [TestClass]
        public class TheGetInstanceMethod
        {
            [ClassInitialize]
            public static void Setup(TestContext context)
            {
                new BootstrapperInitialization().Start();
            }

            [TestMethod]
            public void it_should_return_a_valid_object_when_the_type_is_CommandLineOptions()
            {
                var sut = ServiceLocator.Current.GetInstance<CommandLineOptions>();

                sut.Should().NotBeNull();
            }

            [TestMethod]
            public void it_should_return_a_valid_object_when_the_type_is_ICommandLineParser()
            {
                ServiceLocator.Current.GetInstance<ICommandLineParser>().Should().NotBeNull();
            }

            [TestMethod]
            public void it_should_return_a_valid_object_when_the_type_is_ArgumentsValidator()
            {
                ServiceLocator.Current.GetInstance<ArgumentsValidator>().Should().NotBeNull();
            }

            [TestMethod]
            public void it_should_return_a_valid_object_when_the_type_is_TargetsCodeGeneratorController()
            {
                ServiceLocator.Current.GetInstance<TargetsCodeGeneratorController>().Should().NotBeNull();
            }

            [TestMethod]
            public void it_should_return_a_valid_object_when_the_type_is_ApplicationController()
            {
                ServiceLocator.Current.GetInstance<ApplicationController>().Should().NotBeNull();
            }

            [TestMethod]
            public void when_asking_the_GetBuildNumberTargetGenerator_type_it_should_return_a_valid_GetBuildNumberTargetGenerator_when_the_CIS_option_is_not_set()
            {
                this.CheckConditionalBinding<GetBuildNumberTargetGenerator, GetBuildNumberTargetGenerator>();
            }

            [TestMethod]
            public void when_asking_the_GetBuildNumberTargetGenerator_type_it_should_return_a_valid_GetBuildNumberFromCcnetTargetsGenerator_when_the_CIS_option_is_CCNET()
            {
                this.CheckConditionalBinding<GetBuildNumberFromCcnetTargetsGenerator, GetBuildNumberTargetGenerator>("--cis", "ccnet");
            }

            [TestMethod]
            public void when_asking_the_GetBuildNumberTargetGenerator_type_it_should_return_a_valid_GetBuildNumberFromHudsonTargetGenerator_when_the_CIS_option_is_Hudson()
            {
                this.CheckConditionalBinding<GetBuildNumberFromHudsonTargetGenerator, GetBuildNumberTargetGenerator>("--cis", "hudson");
            }

            [TestMethod]
            public void when_asking_the_GetBuildNumberTargetGenerator_type_it_should_return_a_valid_GetBuildNumberFromTeamCityTargetsGenerator_when_the_CIS_option_is_Teamcity()
            {
                this.CheckConditionalBinding<GetBuildNumberFromTeamCityTargetsGenerator, GetBuildNumberTargetGenerator>("--cis", "teamcity");
            }

            [TestMethod]
            public void when_asking_the_GetBuildNumberTargetGenerator_type_it_should_return_a_valid_GetBuildNumberFromTfsTargetsGenerator_when_the_CIS_option_is_TFS()
            {
                this.CheckConditionalBinding<GetBuildNumberFromTfsTargetsGenerator, GetBuildNumberTargetGenerator>("--cis", "tfs");
            }

            [TestMethod]
            public void when_asking_the_GetRevisionVersionTargetGenerator_type_it_should_return_a_valid_GetRevisionVersionTargetGenerator_when_the_VCS_options_is_not_set()
            {
                this.CheckConditionalBinding<GetRevisionVersionTargetGenerator, GetRevisionVersionTargetGenerator>();
            }

            [TestMethod]
            public void when_asking_the_GetRevisionVersionTargetGenerator_type_it_should_return_a_valid_GetRevisionVersionFromGitTargetGenerator_when_the_VCS_options_is_Git()
            {
                this.CheckConditionalBinding<GetRevisionVersionFromGitTargetGenerator, GetRevisionVersionTargetGenerator>("--vcs", "git");
            }

            [TestMethod]
            public void when_asking_the_GetRevisionVersionTargetGenerator_type_it_should_return_a_valid_GetRevisionVersionFromSvnTargetGenerator_when_the_VCS_options_is_SVN()
            {
                this.CheckConditionalBinding<GetRevisionVersionFromSvnTargetGenerator, GetRevisionVersionTargetGenerator>("--vcs", "svn");
            }

            [TestMethod]
            public void when_asking_the_GetRevisionVersionTargetGenerator_type_it_should_return_a_valid_GetRevisionVersionFromTfsTargetGenerator_when_the_VCS_options_is_TFS()
            {
                this.CheckConditionalBinding<GetRevisionVersionFromTfsTargetGenerator, GetRevisionVersionTargetGenerator>("--vcs", "tfs");
            }

            [TestMethod]
            public void when_asking_the_GetAdditionalInformationalVersionTargetGenerator_type_it_should_return_a_valid_GetAdditionalInformationalVersionTargetGenerator_when_the_VCS_options_is_not_set()
            {
                this.CheckConditionalBinding<GetAdditionalInformationalVersionTargetGenerator, GetAdditionalInformationalVersionTargetGenerator>();
            }

            [TestMethod]
            public void when_asking_the_GetAdditionalInformationalVersionTargetGenerator_type_it_should_return_a_valid_GetAdditionalInformationalVersionFromGitTargetGenerator_when_the_VCS_options_is_Git()
            {
                this.CheckConditionalBinding<GetAdditionalInformationalVersionFromGitTargetGenerator, GetAdditionalInformationalVersionTargetGenerator>("--vcs", "git");
            }

            [TestMethod]
            public void when_asking_the_GetAdditionalInformationalVersionTargetGenerator_type_it_should_return_a_valid_GetAdditionalInformationalVersionFromSvnTargetGenerator_when_the_VCS_options_is_SVN()
            {
                this.CheckConditionalBinding<GetAdditionalInformationalVersionFromSvnTargetGenerator, GetAdditionalInformationalVersionTargetGenerator>("--vcs", "svn");
            }

            [TestMethod]
            public void when_asking_the_GetAdditionalInformationalVersionTargetGenerator_type_it_should_return_a_valid_GetAdditionalInformationalVersionFromTfsTargetGenerator_when_the_VCS_options_is_TFS()
            {
                this.CheckConditionalBinding<GetAdditionalInformationalVersionFromTfsTargetGenerator, GetAdditionalInformationalVersionTargetGenerator>("--vcs", "tfs");
            }

            [TestMethod]
            public void when_asking_the_VcsRunnerPropertiesGeneratorProvider_type_it_should_return_a_valid_VcsRunnerPropertiesGenerator_when_the_VCS_options_is_not_set()
            {
                this.CheckConditionalBinding<VcsRunnerPropertiesGenerator, VcsRunnerPropertiesGenerator>();
            }

            [TestMethod]
            public void when_asking_the_VcsRunnerPropertiesGeneratorProvider_type_it_should_return_a_valid_GitRunnerPropertiesGenerator_when_the_VCS_options_is_Git()
            {
                this.CheckConditionalBinding<GitRunnerPropertiesGenerator, VcsRunnerPropertiesGenerator>("--vcs", "git");
            }

            [TestMethod]
            public void when_asking_the_VcsRunnerPropertiesGeneratorProvider_type_it_should_return_a_valid_SvnRunnerPropertiesGenerator_when_the_VCS_options_is_SVN()
            {
                this.CheckConditionalBinding<SvnRunnerPropertiesGenerator, VcsRunnerPropertiesGenerator>("--vcs", "svn");
            }

            [TestMethod]
            public void when_asking_the_VcsRunnerPropertiesGeneratorProvider_type_it_should_return_a_valid_TfsRunnerPropertiesGenerator_when_the_VCS_options_is_TFS()
            {
                this.CheckConditionalBinding<TfsRunnerPropertiesGenerator, VcsRunnerPropertiesGenerator>("--vcs", "tfs");
            }

            private void CheckConditionalBinding<TExpected, TSource>(params string[] arguments)
                where TExpected : class
                where TSource : class
            {
                List<string> args = new List<string> { "-p", "MyApp", "-o", "." };

                if (arguments != null)
                {
                    args.AddRange(arguments);
                }

                new BootstrapperInitialization().Start();
                var validator = ServiceLocator.Current.GetInstance<ArgumentsValidator>().AreArgumentsValid(args.ToArray());
                var expected = ServiceLocator.Current.GetInstance<TSource>();

                expected.Should()
                    .NotBeNull()
                    .And.BeAssignableTo<TSource>()
                    .And.BeOfType<TExpected>();
            }
        }
    }
}
