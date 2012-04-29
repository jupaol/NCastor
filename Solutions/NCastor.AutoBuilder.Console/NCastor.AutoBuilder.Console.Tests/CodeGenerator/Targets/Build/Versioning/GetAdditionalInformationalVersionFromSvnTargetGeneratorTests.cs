﻿using System;
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
    public class GetAdditionalInformationalVersionFromSvnTargetGeneratorTests
    {
        [TestMethod]
        public void can_create_a_new_instance_when_using_valid_CommandLineOptions()
        {
            var sut = new GetAdditionalInformationalVersionFromSvnTargetGenerator(new CommandLineOptions());

            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void it_should_return_the_generated_code_to_get_additional_informational_version_from_SVN()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<GetAdditionalInformationalVersionFromSvnTargetGenerator>();
                var res = sut.GenerateCode();

                res.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("CoreGetExtraInformationalVersion")
                    .And.Contain("FormatSvnRevisionVersion");

                res.CountOcurrences("CoreGetExtraInformationalVersion").Should().Be(1);
                res.CountOcurrences("FormatSvnRevisionVersion").Should().Be(1);
            }
        }
    }
}