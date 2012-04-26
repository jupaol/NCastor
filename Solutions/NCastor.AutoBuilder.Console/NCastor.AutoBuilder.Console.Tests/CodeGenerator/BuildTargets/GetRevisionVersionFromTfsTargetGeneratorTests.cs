using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCastor.AutoBuilder.Console.CodeGenerator.BuildTargets;
using FluentAssertions;
using Ploeh.AutoFixture;

namespace NCastor.AutoBuilder.Console.Tests.CodeGenerator.BuildTargets
{
    [TestClass]
    public class GetRevisionVersionFromTfsTargetGeneratorTests
    {
        [TestMethod]
        public void can_create_a_new_instance_with_valid_CommandLineOptions()
        {
            var sut = new GetRevisionVersionFromTfsTargetGenerator(new CommandLineOptions());

            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void it_should_return_the_targets_generated_code_to_get_the_revision_version_from_tfs()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<GetRevisionVersionFromTfsTargetGenerator>();
                var res = sut.GenerateCode();

                res.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreGetRevisionVersion")
                    .And.Contain("UseTFSRevisionAsCurrentRevisionVersion");

                res.CountOcurrences("CoreGetRevisionVersion").Should().Be(1);
                res.CountOcurrences("UseTFSRevisionAsCurrentRevisionVersion").Should().Be(1);
            }
        }
    }
}
