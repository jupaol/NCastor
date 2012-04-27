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
    public class SvnRunnerPropertiesGeneratorTests
    {
        [TestClass]
        public class TheGenerateCodeMethod
        {
            [TestMethod]
            public void it_hsould_return_the_generated_code_to_use_the_SVN_runner()
            {
                var sut = new AutoFixtureHelper().Fixture.CreateAnonymous<SvnRunnerPropertiesGenerator>();
                var res = sut.GenerateCode();

                res.Should()
                    .NotBeNullOrEmpty()
                    .And.NotBeBlank()
                    .And.NotContain("<!--<SvnRunnersPath>")
                    .And.NotContain("</SvnRunnersPath>-->")

                    .And.Contain("<SvnRunnersPath>")
                    .And.Contain("</SvnRunnersPath>")
                    .And.Contain("<!--<GitRunner>")
                    .And.Contain("</GitRunner>-->")
                    .And.Contain("<!--<TFSRunner>")
                    .And.Contain("</TFSRunner>-->");

                res.CountOcurrences("<SvnRunnersPath>").Should().Be(1);
                res.CountOcurrences("</SvnRunnersPath>").Should().Be(1);
                res.CountOcurrences("<!--<GitRunner>").Should().Be(1);
                res.CountOcurrences("</GitRunner>-->").Should().Be(1);
                res.CountOcurrences("<!--<TFSRunner>").Should().Be(1);
                res.CountOcurrences("</TFSRunner>-->").Should().Be(1);
            }
        }
    }
}
