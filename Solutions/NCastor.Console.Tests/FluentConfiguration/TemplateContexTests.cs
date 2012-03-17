using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCastor.Console.FluentConfiguration;

namespace NCastor.Console.Tests.FluentConfiguration
{
    [TestClass]
    public class TemplateContexTests
    {
        #region Tests for method Options

        [TestMethod]
        public void when_calling_options_with_a_null_options_parameter_it_should_throw_an_ArgumentNullException()
        {
            var cfg = new TemplateConfigurator();

            Action invoking = () => cfg.Context.Establish.Options(null);

            invoking
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("options", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void when_calling_Options_with_a_valid__object_it_should_assign_it_to_the_CurrentOptions_property()
        {
            var cfg = new TemplateConfigurator();
            var opt = new CommandLineOptions();

            cfg.Invoking(x => x.Context.Establish.Options(opt))
                .ShouldNotThrow();

            cfg.Context.CurrentOptions.Should().NotBeNull();
        }

        [TestMethod]
        public void when_calling_options_with_a_valid_object_it_should_return_the_parent_configurator_object()
        {
            var cfg = new TemplateConfigurator();
            var opt = new CommandLineOptions();

            var currentCfg = cfg.Context.Establish.Options(opt);

            currentCfg.Should().Be(cfg);
        }

        #endregion
    }
}
