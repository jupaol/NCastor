using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CommandLine;
using CommandLine.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NCastor.AutoBuilder.Console.Tests
{
    [TestClass]
    public class ParserServiceTests
    {
        [TestMethod]
        public void when_specifying_the_ProductName_argument_with_a_value_the_parser_method_should_return_true()
        {
            new ParserHelper().Parse().Should().BeTrue();
        }

        [TestMethod]
        public void when_specifying_the_ProductName_argument_without_a_value_the_parser_should_return_false()
        {
            new ParserHelper().ClearArguments().WithDefaultOutputPath().Parse("-p").Should().BeFalse();
        }

        [TestMethod]
        public void when_specifying_an_invalid_argument_the_parser_should_return_false()
        {
            new ParserHelper().ClearArguments().WithDefaultOutputPath().Parse("--de").Should().BeFalse();
        }

        [TestMethod]
        public void when_the_argument_list_is_empty_the_parser_should_return_false()
        {
            new ParserHelper().ClearArguments().Parse().Should().BeFalse();
        }

        [TestMethod]
        public void when_the_parser_fails_the_help_object_should_return_a_help_message_containing_all_command_line_options_declared()
        {
            new ParserHelper().GetOptionsHelp().Should()
                .Contain("-p <ProductName> your product name")
                .And.Contain("Displays this help text");
        }

        [TestMethod]
        public void when_specifying_the_solution_path_with_a_valid_value_the_parser_should_return_true()
        {
            new ParserHelper().Parse("-s", "solpath").Should().BeTrue();
        }

        [TestMethod]
        public void when_specifying_the_solution_path_with_incomplete_arguments_the_parser_should_return_false()
        {
            new ParserHelper().Parse("-s").Should().BeFalse();
        }

        [TestMethod]
        public void when_not_specifying_the_solution_path_the_parser_should_return_true_because_it_is_optional()
        {
            new ParserHelper().Parse().Should().BeTrue();
        }

        [TestMethod]
        public void calling_ParseArguments_when_specifying_the_GetBuildNumberFrom_option_with_a_valid_parameter_it_should_return_true()
        {
            new ParserHelper().Parse("--CIS", "Hudson").Should().BeTrue();
        }

        [TestMethod]
        public void calling_ParseArguments_when_specifying_the_GetBuildNumberFrom_option_without_a_parameter_it_should_return_false()
        {
            new ParserHelper().Parse("--CIS").Should().BeFalse();
        }

        [TestMethod]
        public void calling_ParseArguments_when_specifying_the_VCS_option_without_a_parameter_it_should_return_false()
        {
            new ParserHelper().Parse("--vCs").Should().BeFalse();
        }

        [TestMethod]
        public void calling_ParseArguments_when_specifying_the_VCS_option_with_an_invalid_parameter_it_should_return_false()
        {
            new ParserHelper().Parse("--VcS", "Gites").Should().BeFalse();
        }

        [TestMethod]
        public void calling_ParseArguments_when_specifying_the_VCS_option_with_a_vlaid_argument_it_should_return_true()
        {
            new ParserHelper().Parse("--vCs", "Git").Should().BeTrue();
        }
    }

    class ParserHelper
    {
        CommandLineOptions Options;
        ICommandLineParser parser;
        List<string> arguments;

        public ParserHelper()
        {
            this.Options = new CommandLineOptions();
            this.parser = this.GetParser();
            this.arguments = new List<string>();
            this.WithDefaultOutputPath();
            this.WithDefaultProductName();
        }

        public string GetOptionsHelp()
        {
            return this.Options.GetHelp();
        }

        public ParserHelper WithDefaultProductName()
        {
            this.arguments.Add("-p MyProduct");
            return this;
        }

        public ParserHelper WithDefaultOutputPath()
        {
            this.arguments.Add("-o .");
            return this;
        }

        public ParserHelper ClearArguments()
        {
            this.arguments.Clear();
            return this;
        }

        public bool Parse(params string[] arguments)
        {
            this.arguments.AddRange(arguments);

            return this.parser.ParseArguments(this.arguments.ToArray(), this.Options);
        }

        private CommandLineParser GetParser()
        {
            return new CommandLineParser(new CommandLineParserSettings { MutuallyExclusive = true, CaseSensitive = false });
        }
    }
}
