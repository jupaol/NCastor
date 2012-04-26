using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using NCastor.AutoBuilder.Console.CodeGenerator.Targets.Build.Versioning;
using FluentAssertions;

namespace NCastor.AutoBuilder.Console.Tests.CodeGenerator.Targets.Build.Versioning
{
    [TestClass]
    public class GetBuildNumberFromHudsonTargetGeneratorTests
    {
        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void should_return_the_generated_targets_code_using_Hudson_as_the_current_CIS()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<GetBuildNumberFromHudsonTargetGenerator>();

                sut.GenerateCode().Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("<Target Name=\"CoreGetBuildVersion\" DependsOnTargets=\"GetBuildVersionFromHudson;\" />");
            }
        }
    }
}
