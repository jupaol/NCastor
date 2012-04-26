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
    public class GetAdditionalInformationalVersionFromTfsTargetGeneratorTests
    {
        [TestMethod]
        public void can_create_a_new_instance_when_using_valid_CommandLineOptions()
        {
            var sut = new GetAdditionalInformationalVersionFromTfsTargetGenerator(new CommandLineOptions());

            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void it_should_return_the_generated_code_to_get_additional_informational_version_from_TFS()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<GetAdditionalInformationalVersionFromTfsTargetGenerator>();
                var res = sut.GenerateCode();

                res.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreGetExtraInformationalVersion")
                    .And.Contain("FormatTFSRevisionVersion");

                res.CountOcurrences("CoreGetExtraInformationalVersion").Should().Be(1);
                res.CountOcurrences("FormatTFSRevisionVersion").Should().Be(1);
            }
        }
    }
}
