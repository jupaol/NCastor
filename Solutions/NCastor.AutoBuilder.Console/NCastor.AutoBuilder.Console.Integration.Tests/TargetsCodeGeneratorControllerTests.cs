using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using NCastor.AutoBuilder.Console.CodeGenerator;
using NCastor.AutoBuilder.Console.Constants;
using NCastor.AutoBuilder.Console.CodeGenerator.BuildTargets;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using NCastor.AutoBuilder.Console;

namespace NCastor.AutoBuilder.Console.Integration.Tests
{
    [TestClass]
    public class TargetsCodeGeneratorControllerTests
    {
        [TestMethod]
        public void can_create_a_new_instance_of_the_targets_code_generator_object()
        {
            TargetsCodeGeneratorController sut = new TargetsCodeGeneratorControllerBuilder();

            sut.Should().NotBeNull();
        }

        [TestMethod]
        public void trying_to_create_a_new_instance_of_TargetsCodeGenerator_with_null_options_should_throw_an_ArgumentNullException()
        {
            TargetsCodeGeneratorController sut = null;
            Action invoking = () => sut = new TargetsCodeGeneratorController(null, null);

            invoking.ShouldThrow<ArgumentNullException>();
        }

        [TestClass]
        public class TheCreateDefaultVersioningTargetsCodeMethod
        {
            [TestMethod]
            public void it_should_returns_the_default_versioning_targets_code()
            {
                TargetsCodeGeneratorController sut = new TargetsCodeGeneratorControllerBuilder();

                sut.CreateDefaultVersioningTargetsCode().Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreFormatSemanticVersion")
                    .And.Contain("CoreFormatFileVersion")
                    .And.Contain("CoreFormatInformationalVersion")
                    .And.Contain("<!--**********Common versioning targets-->")
                    .And.Contain("<!--**********End common versioning targets-->");
            }
        }

        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void when_not_specifying_a_CIS_it_should_return_the_default_versioning_targets_code_and_the_generic_versioning_targets_code()
            {
                TargetsCodeGeneratorController sut = new TargetsCodeGeneratorControllerBuilder();

                sut.GenerateCode().Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreFormatSemanticVersion")
                    .And.Contain("CoreFormatFileVersion")
                    .And.Contain("CoreFormatInformationalVersion")
                    .And.Contain("<!--**********Common versioning targets-->")
                    .And.Contain("<!--**********End common versioning targets-->")
                    .And.Contain("CoreGetBuildVersion")
                    .And.Subject.CountOcurrences("CoreGetBuildVersion").Should()
                        .Be(2);
            }
        }
    }

    class TargetsCodeGeneratorControllerBuilder
    {
        private CommandLineOptions globalOptions;
        private AutoFixtureHelper fixtureHelper;

        public TargetsCodeGeneratorControllerBuilder()
        {
            this.globalOptions = new CommandLineOptions();
            this.fixtureHelper = new AutoFixtureHelper();
        }

        public TargetsCodeGeneratorControllerBuilder WithOptions(CommandLineOptions options)
        {
            this.globalOptions = options;
            return this;
        }

        public TargetsCodeGeneratorControllerBuilder UpdateOptions(Action<CommandLineOptions> updatingOptions)
        {
            updatingOptions(this.globalOptions);
            return this;
        }

        public TargetsCodeGeneratorControllerBuilder UpdateFixture(Action<IFixture> updatingFixturDelegate)
        {
            this.fixtureHelper.CustomizeFixture(updatingFixturDelegate);
            return this;
        }

        public static implicit operator TargetsCodeGeneratorController(TargetsCodeGeneratorControllerBuilder converter)
        {
            converter.fixtureHelper.Fixture.Register<CommandLineOptions>(() => converter.globalOptions);

            TargetsCodeGeneratorController generator = converter.fixtureHelper.Fixture.CreateAnonymous<TargetsCodeGeneratorController>();

            return generator;
        }
    }
}
