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
    public class GetBuildNumberFromTfsTargetsGeneratorTests
    {
        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void it_should_return_the_generated_targets_code_to_get_the_build_number_from_TFS_CIS()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<GetBuildNumberFromTfsTargetsGenerator>();

                sut.GenerateCode().Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("<Target Name=\"CoreGetBuildVersion\" DependsOnTargets=\"GetBuildVersionFromTFS;\" />");
            }
        }
    }
}
