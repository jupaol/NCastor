using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Ploeh.AutoFixture;
using NCastor.AutoBuilder.Console.CodeGenerator.Properties.Runners;
using Moq;
using NCastor.AutoBuilder.Console.CodeGenerator;

namespace NCastor.AutoBuilder.Console.Tests.CodeGenerator.Properties.Runners
{
    [TestClass]
    public class RunnerPropertiesGeneratorControllerTests
    {
        [TestMethod]
        public void can_create_a_new_instance()
        {
            var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<RunnerPropertiesGeneratorController>();

            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void it_should_return_the_generic_properties_generated_code_when_the_VIS_options_is_not_set()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<RunnerPropertiesGeneratorController>();
                var res = sut.GenerateCode();

                res.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("<!--TODO: uncomment the runners that you want to use-->");

                res.CountOcurrences("<!--TODO: uncomment the runners that you want to use-->").Should().Be(1);
            }

            [TestMethod]
            public void it_should_call_GenerateCode_from_the_current_VcsRunnerPropertiesGenerator_object()
            {
                new NestedCodeGeneratorsExecutedHelper().CheckMethodCalled<VcsRunnerPropertiesGenerator, RunnerPropertiesGeneratorController>();
            }
        }
    }
}
