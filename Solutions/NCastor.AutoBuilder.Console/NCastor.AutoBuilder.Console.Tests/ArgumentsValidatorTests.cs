using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using CommandLine;

namespace NCastor.AutoBuilder.Console.Tests
{
    [TestClass]
    public class ArgumentsValidatorTests
    {
        [TestMethod]
        public void can_create_a_new_instance_of_the_ArgumentsValidator_object()
        {
            var sut = new ArgumentsValidator(null);

            sut.Should().NotBeNull();
        }

        [TestMethod]
        public void calling_AreArgumentsValid_with_valid_arguments_it_should_return_true()
        {
            new ArgumentsValidatorHelper().AreArgumentsValid().Should().BeTrue();
        }

        [TestMethod]
        public void calling_AreArgumentsValid_with_the_argument_GetBuildNumberFrom_with_an_invalid_parameter_it_should_return_false()
        {
            new ArgumentsValidatorHelper().AreArgumentsValid("--GetBuildNumberFrom", "212121").Should().BeFalse();
            new ArgumentsValidatorHelper().AreArgumentsValid("--GetBuildNumberFrom", "Hudson2").Should().BeFalse();
        }

        [TestMethod]
        public void calling_AreArgumentsValid_with_the_argument_GetBuildNumberFrom_with_a_valid_parameter_it_should_return_true()
        {
            new ArgumentsValidatorHelper().AreArgumentsValid("--GetBuildNumberFrom", "hudSON").Should().BeTrue();
        }

        [TestMethod]
        public void calling_AreArgumentsValid_with_the_argument_VCS_with_invalid_parameters_it_should_return_false()
        {
            new ArgumentsValidatorHelper().AreArgumentsValid("--VcS", "2121").Should().BeFalse();
            new ArgumentsValidatorHelper().AreArgumentsValid("--VcS", "gIt32").Should().BeFalse();
        }

        [TestMethod]
        public void calling_AreArgumentsValid_with_the_argument_VCS_with_a_valid_parameter_it_should_return_true()
        {
            new ArgumentsValidatorHelper().AreArgumentsValid("--VcS", "gIt").Should().BeTrue();
        }
    }

    class ArgumentsValidatorHelper
    {
        private List<string> arguments;

        public ArgumentsValidatorHelper()
        {
            this.arguments = new List<string>();

            this.arguments.Add("-p");
            this.arguments.Add("MyProduct");
            this.arguments.Add("-o");
            this.arguments.Add(".");
        }

        public ArgumentsValidatorHelper ClearArguments()
        {
            this.arguments.Clear();
            return this;
        }

        public bool AreArgumentsValid(params string[] arguments)
        {
            ICommandLineParser parser = new CommandLineParser(new CommandLineParserSettings { MutuallyExclusive = true, CaseSensitive = false });
            var options = new CommandLineOptions();

            this.arguments.AddRange(arguments);

            return new ArgumentsValidator(parser).AreArgumentsValid(options, this.arguments.ToArray());
        }
    }
}
