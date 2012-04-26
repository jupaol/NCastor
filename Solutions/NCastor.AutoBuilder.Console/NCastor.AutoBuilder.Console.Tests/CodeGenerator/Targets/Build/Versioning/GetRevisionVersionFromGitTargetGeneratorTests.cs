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
    public class GetRevisionVersionFromGitTargetGeneratorTests
    {
        [TestMethod]
        public void can_create_a_new_instance_when_specifying_the_CommandLineOptions()
        {
            var sut = new GetRevisionVersionFromGitTargetGenerator(new CommandLineOptions());

            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void it_should_return_the_generated_targets_code_to_get_the_revision_version_from_Git()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<GetRevisionVersionFromGitTargetGenerator>();

                var res = sut.GenerateCode();

                res.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank();
            }
        }
    }
}
