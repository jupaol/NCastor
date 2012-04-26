using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Reflection;
using NCastor.AutoBuilder.Console.CodeGenerator.BuildTargets;

namespace NCastor.AutoBuilder.Console.Tests
{
    [TestClass]
    public class ApplicationControllerFactoryTests
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            new BootstrapperInitialization().Start();
        }

        [TestMethod]
        public void can_create_a_new_instance()
        {
            var sut = new ApplicationControllerFactoryHelper().Build();

            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheCreateMethod
        {
            [TestMethod]
            public void it_should_return_an_ApplicationController_object_with_a_reference_to_GetBuildNumberTargetGenerator_when_the_GetBuildNumberFrom_option_is_not_set()
            {
                var controller = new ApplicationControllerFactoryHelper().Create(null, "-p", "MyApp", "-o", ".");

                var currentBuildNumberTargetsGenerator = this.CheckPrivateType<TargetsCodeGeneratorController, ApplicationController>("targetsCodeGeneratorController", controller);
                this.CheckPrivateType<GetBuildNumberTargetGenerator, TargetsCodeGeneratorController>("getBuildNumberTargetGenerator", currentBuildNumberTargetsGenerator);
            }

            [TestMethod]
            public void it_should_return_an_ApplicationController_object_with_a_reference_to_GetBuildNumberFromHudsonTargetGenerator_when_the_GetBuildNumberFrom_option_is_set_to_Hudson()
            {
                var controller = new ApplicationControllerFactoryHelper().Create(x => x.ContinuousIntegrationServer = ContinuousIntegrationServers.Hudson, "-p", "MyApp", "-o", ".");

                var currentBuildNumberTargetsGenerator = this.CheckPrivateType<TargetsCodeGeneratorController, ApplicationController>("targetsCodeGeneratorController", controller);
                this.CheckPrivateType<GetBuildNumberFromHudsonTargetGenerator, TargetsCodeGeneratorController>("getBuildNumberTargetGenerator", currentBuildNumberTargetsGenerator);
            }

            [TestMethod]
            public void it_should_return_an_ApplicationController_object_with_a_reference_to_GetBuildNumberFromTeamCityTargetGenerator_when_the_GetBuildNumberFrom_option_is_set_to_TeamCity()
            {
                var controller = new ApplicationControllerFactoryHelper().Create(x => x.ContinuousIntegrationServer = ContinuousIntegrationServers.TeamCity, "-p", "MyApp", "-o", ".");

                var currentBuildNumberTargetsGenerator = this.CheckPrivateType<TargetsCodeGeneratorController, ApplicationController>("targetsCodeGeneratorController", controller);
                this.CheckPrivateType<GetBuildNumberFromTeamCityTargetsGenerator, TargetsCodeGeneratorController>("getBuildNumberTargetGenerator", currentBuildNumberTargetsGenerator);
            }

            [TestMethod]
            public void it_should_return_an_ApplicationController_object_with_a_reference_to_GetBuildNumberFromCcnetTargetGenerator_when_the_GetBuildNumberFrom_option_is_set_to_CCNET()
            {
                var controller = new ApplicationControllerFactoryHelper().Create(x => x.ContinuousIntegrationServer = ContinuousIntegrationServers.CCNET, "-p", "MyApp", "-o", ".");

                var currentBuildNumberTargetsGenerator = this.CheckPrivateType<TargetsCodeGeneratorController, ApplicationController>("targetsCodeGeneratorController", controller);
                this.CheckPrivateType<GetBuildNumberFromCcnetTargetsGenerator, TargetsCodeGeneratorController>("getBuildNumberTargetGenerator", currentBuildNumberTargetsGenerator);
            }

            [TestMethod]
            public void it_should_return_an_ApplicationController_object_with_a_reference_to_GetBuildNumberFromTfsTargetGenerator_when_the_GetBuildNumberFrom_option_is_set_to_TFS()
            {
                var controller = new ApplicationControllerFactoryHelper().Create(x => x.ContinuousIntegrationServer = ContinuousIntegrationServers.TFS, "-p", "MyApp", "-o", ".");

                var currentBuildNumberTargetsGenerator = this.CheckPrivateType<TargetsCodeGeneratorController, ApplicationController>("targetsCodeGeneratorController", controller);
                this.CheckPrivateType<GetBuildNumberFromTfsTargetsGenerator, TargetsCodeGeneratorController>("getBuildNumberTargetGenerator", currentBuildNumberTargetsGenerator);
            }

            private TExpected CheckPrivateType<TExpected, TSource>(string field, TSource source, BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance)
                where TExpected : class
            {
                var currentFieldValue = source.GetType().GetField(field, bindingFlags).GetValue(source);

                currentFieldValue.Should().NotBeNull();
                currentFieldValue.Should().BeOfType<TExpected>();

                return currentFieldValue as TExpected;
            }
        }
    }

    class ApplicationControllerFactoryHelper
    {
        public ApplicationControllerFactory Build()
        {
            var factory = new ApplicationControllerFactory();

            return factory;
        }

        public ApplicationController Create(Action<CommandLineOptions> updatingOptionsDelegate, params string[] arguments)
        {
            var options = new CommandLineOptions();

            if (updatingOptionsDelegate != null)
            {
                updatingOptionsDelegate(options);
            }

            return this.Build().Create(arguments, options);
        }
    }
}
