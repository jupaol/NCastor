using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Ploeh.AutoFixture;
using NCastor.AutoBuilder.Console.CodeGenerator.BuildTargets;

namespace NCastor.AutoBuilder.Console.Tests.CodeGenerator.BuildTargets
{
    [TestClass]
    public class GetRevisionVersionTargetsGeneratorTests
    {
        [TestMethod]
        public void can_createa_new_instance()
        {
            var sut = new GetRevisionVersionTargetGenerator(new CommandLineOptions());

            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void it_should_return_the_generic_generated_targets_code()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<GetRevisionVersionTargetGenerator>();

                var res = sut.GenerateCode();
                
                res.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreGetRevisionVersion");

                res.CountOcurrences("CoreGetRevisionVersion").Should().Be(2);
            }
        }
    }
}
