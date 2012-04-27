using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Ploeh.AutoFixture;
using NCastor.AutoBuilder.Console.CodeGenerator.Properties.Runners;

namespace NCastor.AutoBuilder.Console.Tests.CodeGenerator.Properties.Runners
{
    [TestClass]
    public class GitRunnerPropertiesGeneratorTests
    {
        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void it_should_return_the_generated_code_of_the_Git_runner_properties()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<GitRunnerPropertiesGenerator>();
                var res = sut.GenerateCode();

                res.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.NotContain("<!--<GitRunner>")
                    .And.NotContain("</GitRunner>-->")
                    .And.Contain("<GitRunner>")
                    .And.Contain("</GitRunner>")
                    .And.Contain("<!--<SvnRunnersPath>")
                    .And.Contain("</SvnRunnersPath>-->")
                    .And.Contain("<!--<TFSRunner>")
                    .And.Contain("</TFSRunner>-->");

                res.CountOcurrences("<GitRunner>").Should().Be(1);
                res.CountOcurrences("</GitRunner>").Should().Be(1);
                res.CountOcurrences("<!--<SvnRunnersPath>").Should().Be(1);
                res.CountOcurrences("</SvnRunnersPath>-->").Should().Be(1);
                res.CountOcurrences("<!--<TFSRunner>").Should().Be(1);
                res.CountOcurrences("</TFSRunner>-->").Should().Be(1);
            }
        }
    }
}
