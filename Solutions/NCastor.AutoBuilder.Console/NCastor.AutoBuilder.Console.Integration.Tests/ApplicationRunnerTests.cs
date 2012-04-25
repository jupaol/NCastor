using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using FluentAssertions.EventMonitoring;
using System.IO;
using NCastor.AutoBuilder.Console.Constants;

namespace NCastor.AutoBuilder.Console.Integration.Tests
{
    [TestClass]
    public class ApplicationRunnerTests
    {
        [TestMethod]
        public void can_create_a_new_instance()
        {
            var sut = new ApplicationRunner();

            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheRunMethod
        {
            [TestMethod]
            public void it_should_raise_the_ArgumentsValidationFailed_event_when_the_arguments_are_invalid()
            {
                var sut = new ApplicationRunner();

                sut.MonitorEvents();
                sut.Run(new[] { "-p" });

                sut.ShouldRaise("ArgumentsValidationFailed");
            }

            [TestMethod]
            public void it_should_raise_the_ErrorOcurred_event_when_an_unexpected_exception_ocurrs()
            {
                var sut = new ApplicationRunner();

                sut.MonitorEvents();
                sut.Run(null);

                sut.ShouldRaise("ErrorOcurred");
            }

            [TestMethod]
            public void it_should_create_the_solutions_temmplate_when_the_arguments_are_valid()
            {
                var sut = new ApplicationRunner();

                sut.MonitorEvents();
                sut.Run(new[] { "-p", "MyApp", "-o", "." });

                sut.ShouldNotRaise("ArgumentsValidationFailed");
                sut.ShouldNotRaise("ErrorOcurred");
                File.Exists(".\\MyApp." + TemplateConstants.Solution.Replace(".template", string.Empty)).Should().BeTrue();
            }

            [TestMethod]
            public void it_should_create_the_custom_properties_temmplate_when_the_arguments_are_valid()
            {
                var sut = new ApplicationRunner();

                sut.MonitorEvents();
                sut.Run(new[] { "-p", "MyApp", "-o", "." });

                sut.ShouldNotRaise("ArgumentsValidationFailed");
                sut.ShouldNotRaise("ErrorOcurred");
                File.Exists(".\\MyApp." + TemplateConstants.CustomSolutionProperties.Replace(".template", string.Empty)).Should().BeTrue();
            }

            [TestMethod]
            public void it_should_create_the_custom_targets_temmplate_when_the_arguments_are_valid()
            {
                var sut = new ApplicationRunner();

                sut.MonitorEvents();
                sut.Run(new[] { "-p", "MyApp", "-o", "." });

                sut.ShouldNotRaise("ArgumentsValidationFailed");
                sut.ShouldNotRaise("ErrorOcurred");
                File.Exists(".\\MyApp." + TemplateConstants.CustomSolutionTargets.Replace(".template", string.Empty)).Should().BeTrue();
            }
        }
    }
}
