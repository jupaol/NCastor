using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using CommandLine.Text;
using System.Reflection;
using CommandLine;

namespace NCastor.Console.Tests
{
    [TestClass]
    public class ParserServiceTests
    {
        [TestMethod]
        public void when_specifying_the_ProductName_argument_with_a_value_the_parser_method_should_return_true()
        {
            var options = new CommandLineOptions();
            string[] args = new[] { "-p hhh", "-o de" };
            ICommandLineParser parser = new CommandLineParser();

            bool parserResults = parser.ParseArguments(args, options);

            parserResults.Should().BeTrue();
        }

        [TestMethod]
        public void when_specifying_the_ProductName_argument_without_a_value_the_parser_should_return_false()
        {
            var options = new CommandLineOptions();
            string[] args = new[] { "-p", "-o de" };
            ICommandLineParser parser = new CommandLineParser();

            bool parserResults = parser.ParseArguments(args, options);

            parserResults.Should().BeFalse();
        }

        [TestMethod]
        public void when_specifying_an_invalid_argument_the_parser_should_return_false()
        {
            var options = new CommandLineOptions();
            string[] args = new[] { "-de", "-o de" };
            ICommandLineParser parser = new CommandLineParser();

            bool parserResults = parser.ParseArguments(args, options);

            parserResults.Should().BeFalse();
        }

        [TestMethod]
        public void when_the_argument_list_is_empty_the_parser_should_return_false()
        {
            var options = new CommandLineOptions();
            string[] args = new[] { "" };
            ICommandLineParser parser = new CommandLineParser();

            bool parserResults = parser.ParseArguments(args, options);

            parserResults.Should().BeFalse();
        }

        [TestMethod]
        public void when_the_parser_fails_the_help_object_should_return_a_help_message_containing_all_command_line_options_declared()
        {
            var options = new CommandLineOptions();
            string[] args = new[] { "" };

            options.GetHelp().ToString()
                .Should().Contain("-p <ProductName> your product name").And.Contain("Displays this help text");
        }

        [TestMethod]
        public void when_specifying_the_solution_path_with_a_valid_value_the_parser_should_return_true()
        {
            var options = new CommandLineOptions();
            string[] args = new[] { "-s", "solpath", "-p", "product", "-o de" };
            ICommandLineParser parser = new CommandLineParser();

            bool res = parser.ParseArguments(args, options);

            res.Should().BeTrue();
        }

        [TestMethod]
        public void when_specifying_the_solution_path_with_incomplete_arguments_the_parser_should_return_false()
        {
            var options = new CommandLineOptions();
            string[] args = new[] { "-s", "-p", "product", "-o de" };
            ICommandLineParser parser = new CommandLineParser();

            bool res = parser.ParseArguments(args, options);

            res.Should().BeFalse();
        }

        [TestMethod]
        public void when_not_specifying_the_solution_path_the_parser_should_return_true_because_it_is_optional()
        {
            var options = new CommandLineOptions();
            string[] args = new[] { "-p", "product", "-o de" };
            ICommandLineParser parser = new CommandLineParser();

            bool res = parser.ParseArguments(args, options);

            res.Should().BeTrue();
        }
    }
}
