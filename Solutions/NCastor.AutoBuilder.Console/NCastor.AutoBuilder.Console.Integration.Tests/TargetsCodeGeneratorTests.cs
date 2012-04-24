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

namespace NCastor.AutoBuilder.Console.Integration.Tests
{
    [TestClass]
    public class TargetsCodeGeneratorTests
    {
        [TestMethod]
        public void can_create_a_new_instance_of_the_targets_code_generator_object()
        {
            TargetsCodeGeneratorController sut = new TargetsCodeGeneratorBuilder();

            sut.Should().NotBeNull();
        }

        [TestMethod]
        public void trying_to_create_a_new_instance_of_TargetsCodeGenerator_with_null_options_should_throw_an_ArgumentNullException()
        {
            TargetsCodeGeneratorController sut = null;
            Action invoking = () => sut = new TargetsCodeGeneratorController(null, null);

            invoking.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void calling_CreateDefaultVersioningTargetsCode_returns_the_default_versioning_targets_code()
        {
            TargetsCodeGeneratorController sut = new TargetsCodeGeneratorBuilder();

            sut.CreateDefaultVersioningTargetsCode().Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreFormatSemanticVersion")
                    .And.Contain("CoreFormatFileVersion")
                    .And.Contain("CoreFormatInformationalVersion")
                    .And.Contain("<!--**********Common versioning targets-->")
                    .And.Contain("<!--**********End common versioning targets-->");
        }

        //[TestMethod]
        //public void calling_CreateCISTargetsCode_when_specifying_Hudson_as_the_current_CIS_it_should_return_a_template_body_with_the_Hudson_targets()
        //{
        //    TargetsCodeGenerator sut = new TargetsCodeGeneratorBuilder().UpdateOptions((x) => x.GetBuildNumberFrom = ContinuousIntegrationServers.Hudson);

        //    sut.CreateCISTargetsCode(
        //        CodeGeneratorTemplateConstants.,
        //        "",
        //        (x, y, z) => { });
        //}
    }

    class TargetsCodeGeneratorBuilder
    {
        private CommandLineOptions globalOptions;
        private AutoFixtureHelper fixtureHelper;

        public TargetsCodeGeneratorBuilder()
        {
            this.globalOptions = new CommandLineOptions();
            this.fixtureHelper = new AutoFixtureHelper();
        }

        public TargetsCodeGeneratorBuilder WithOptions(CommandLineOptions options)
        {
            this.globalOptions = options;
            return this;
        }

        public TargetsCodeGeneratorBuilder UpdateOptions(Action<CommandLineOptions> updatingOptions)
        {
            updatingOptions(this.globalOptions);
            return this;
        }

        public TargetsCodeGeneratorBuilder UpdateFixture(Action<IFixture> updatingFixturDelegate)
        {
            this.fixtureHelper.CustomizeFixture(updatingFixturDelegate);
            return this;
        }

        public static implicit operator TargetsCodeGeneratorController(TargetsCodeGeneratorBuilder converter)
        {
            converter.fixtureHelper.Fixture.Register<CommandLineOptions>(() => converter.globalOptions);

            TargetsCodeGeneratorController generator = converter.fixtureHelper.Fixture.CreateAnonymous<TargetsCodeGeneratorController>();

            return generator;
        }
    }
}
