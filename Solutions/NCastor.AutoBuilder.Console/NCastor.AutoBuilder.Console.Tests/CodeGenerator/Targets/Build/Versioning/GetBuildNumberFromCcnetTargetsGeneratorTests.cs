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
    public class GetBuildNumberFromCcnetTargetsGeneratorTests
    {
        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void it_should_return_the_generated_targets_code_using_CCNET_as_the_current_CIS()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<GetBuildNumberFromCcnetTargetsGenerator>();

                sut.GenerateCode().Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("<Target Name=\"CoreGetBuildVersion\" DependsOnTargets=\"GetBuildVersionFromCCNET;\" />");
            }
        }
    }
}
