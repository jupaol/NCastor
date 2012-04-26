using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using FluentAssertions;
using NCastor.AutoBuilder.Console.CodeGenerator.Targets.Build.Versioning;

namespace NCastor.AutoBuilder.Console.Tests.CodeGenerator.Targets.Build.Versioning
{
    [TestClass]
    public class GetBuildNumberFromTeamCityTargetsGeneratorTests
    {
        [TestClass]
        public class TheGeneratecodeMethod
        {
            [TestMethod]
            public void it_should_return_the_generated_targets_code_to_get_the_build_number_from_TeamCity_CIS()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<GetBuildNumberFromTeamCityTargetsGenerator>();

                sut.GenerateCode().Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.Contain("<Target Name=\"CoreGetBuildVersion\" DependsOnTargets=\"GetBuildVersionFromTeamCity;\" />");
            }
        }
    }
}
