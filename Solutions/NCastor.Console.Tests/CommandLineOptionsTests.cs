using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NCastor.Console.Tests
{
    [TestClass]
    public class CommandLineOptionsTests
    {
        [TestMethod]
        public void calling_ProcessOptions_when_the_solution_path_is_null_or_emty_it_should_return_an_empty_string()
        {
            var opt = new CommandLineOptions();

            opt.SolutionPath = null;
            opt.ProcessOptions();

            opt.SolutionName.Should().NotBeNull().And.BeEmpty();
        }

        [TestMethod]
        public void calling_ProcessOptions_when_the_solution_path_does_not_exists_it_should_throw_a_FileNotFoundException()
        {
            var opt = new CommandLineOptions();

            opt.SolutionPath = "invalid value";

            opt.Invoking(x =>
                {
                    x.ProcessOptions();
                })
                .ShouldThrow<FileNotFoundException>()
                .WithMessage("The solution path was not found", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void calling_ProcesOptions_when_the_SolutionPath_exists_the_SolutionName_should_return_the_SolutionName_without_extension()
        {
            var opt = new CommandLineOptions();

            opt.SolutionPath = this.GetType().Assembly.Location;

            opt.ProcessOptions();

            opt.SolutionName.Should().Be("NCastor.Console.Tests");
        }
    }
}
