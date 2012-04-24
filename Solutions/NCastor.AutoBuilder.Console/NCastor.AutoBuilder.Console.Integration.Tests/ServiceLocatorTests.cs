using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.ServiceLocation;
using FluentAssertions;
using CommandLine;

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
        }
    }
}
