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
    public class GetAdditionalInformationalVersionTargetGeneratorTests
    {
        [TestMethod]
        public void can_create_a_new_instance_with_valid_CommandLineOptions()
        {
            var sut = new GetAdditionalInformationalVersionTargetGenerator(new CommandLineOptions());

            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void it_should_return_the_generated_targets_code_to_get_the_extra_informational_version_when_the_VCS_option_is_not_set()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<GetAdditionalInformationalVersionTargetGenerator>();
                var res = sut.GenerateCode();

                res.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreGetExtraInformationalVersion");

                res.CountOcurrences("CoreGetExtraInformationalVersion").Should().Be(2);
            }
        }
    }
}
