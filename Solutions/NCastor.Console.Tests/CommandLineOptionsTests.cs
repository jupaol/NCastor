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

            opt.SolutionName = null;
            opt.ProcessOptions();

            opt.SolutionName.Should().NotBeNull().And.BeEmpty();
        }

        [TestMethod]
        public void calling_ProcesOptions_when_the_SolutionName_was_specified_the_solution_name_should_return_the_SolutionName()
        {
            var opt = new CommandLineOptions();

            opt.SolutionName = "NCastor.Console.Tests";

            opt.ProcessOptions();

            opt.SolutionName.Should().Be("NCastor.Console.Tests");
        }
    }
}
