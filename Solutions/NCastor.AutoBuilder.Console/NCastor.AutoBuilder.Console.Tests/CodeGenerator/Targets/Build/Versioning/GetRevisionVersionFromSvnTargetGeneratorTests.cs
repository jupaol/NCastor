using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Ploeh.AutoFixture;
using NCastor.AutoBuilder.Console.CodeGenerator.Targets.Build.Versioning;

namespace NCastor.AutoBuilder.Console.Tests.CodeGenerator.Targets.Build.Versioning
{
    [TestClass]
    public class GetRevisionVersionFromSvnTargetGeneratorTests
    {
        [TestMethod]
        public void can_create_a_new_instance_with_a_valid_CommandLineOptions()
        {
            var sut = new GetRevisionVersionFromSvnTargetGenerator(new CommandLineOptions());

            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void it_should_return_the_generated_targets_code_to_get_the_revision_version_from_svn()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<GetRevisionVersionFromSvnTargetGenerator>();
                var res = sut.GenerateCode();

                res.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreGetRevisionVersion")
                    .And.Contain("UseSvnRevisionAsCurrentRevisionVersion");

                res.CountOcurrences("CoreGetRevisionVersion").Should().Be(1);
                res.CountOcurrences("UseSvnRevisionAsCurrentRevisionVersion").Should().Be(1);
            }
        }
    }
}
