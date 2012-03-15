using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using FluentAssertions;
using TemplateEngine;
using NCastor.Console.FluentConfiguration;
using NCastor.Console.Services;
using Moq;
using System.Reflection;
using CuttingEdge.Conditions;
using NCastor.Console.Constants;

namespace NCastor.Console.Tests.FluentConfiguration
{
    [TestClass]
    public class TemplateFactoryTests
    {
        #region Tests for method SpecificAssemby

        [TestMethod]
        public void when_calling_SpecificAssembly_it_should_set_the_specified_assembly_to_the_workingAssembly_field()
        {
            var cfg = new TemplateConfigurator();
            var asse = typeof(TemplateFactoryTests).Assembly;

            var fact = cfg.Factory.Find.From.Assembly.Using.SpecificAssembly(asse);

            var field = fact.GetType().GetField("workingAssembly", BindingFlags.Instance | BindingFlags.NonPublic);

            (field.GetValue(fact) as Assembly).Should().NotBeNull().And.Be(asse);
        }

        #endregion

        #region Tests for method CurrentAssembly

        [TestMethod]
        public void when_calling_CurrentAssembly_it_should_set_the_current_assembly_in_the_TemplateFactory_workingAssembly_field()
        {
            var cfg = new TemplateConfigurator();

            var fact = cfg.Factory.Find.From.Assembly.Using.CurrentAssembly();

            var field = fact.GetType().GetField("workingAssembly", BindingFlags.Instance | BindingFlags.NonPublic);

            (field.GetValue(fact) as Assembly).Should().NotBeNull().And.Be(typeof(TemplateConfigurator).Assembly);
        }

        #endregion

        #region Tests for method FindEmbeddedResource

        [TestMethod]
        public void calling_FindEmbeddedResource_with_a_null_IAssemblyResourceFinde_parameterr_it_should_throw_an_ArgumentNullException()
        {
            var cfg = new TemplateConfigurator();

            cfg.Invoking(x => x.Factory.Find.From.Assembly.Using.CurrentAssembly().FindEmbeddedResource(null, "whateva", "na"))
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("assemblyResourceFinder", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void calling_FindEmbeddedResource_with_a_null_embeddedFileName_parameter_it_should_throw_an_ArgumentException()
        {
            var cfg = new TemplateConfigurator();
            var servMock = new Mock<IAssemblyResourceFinder>();

            cfg.Invoking(x => x.Factory.Find.From.Assembly.Using.CurrentAssembly().FindEmbeddedResource(servMock.Object, "    ", "na"))
                .ShouldThrow<ArgumentException>()
                .WithMessage("embeddedFileName", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void when_calling_FindEmbeddedResource_without_specifying_a_valid_assembly_it_should_an_ArgumentNullException()
        {
            var cfg = new TemplateConfigurator();
            var servMock = new Mock<IAssemblyResourceFinder>();

            cfg.Invoking(x => x.Factory.Find.From.Assembly.Using.FindEmbeddedResource(servMock.Object, "      cedede  ", "de"))
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("The working assembly must be assigned before calling FindEmbeddedResource", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void when_calling_FindEmbeddedResource_it_should_set_the_Assembly_property_of_the_IAssemblyResourceFinder()
        {
            var cfg = new TemplateConfigurator();
            var servMock = new Mock<IAssemblyResourceFinder>();
            var assembly = typeof(IAssemblyResourceFinder).Assembly;

            servMock.Setup(x => x.FindResource<Stream>(It.IsAny<string>())).Returns(new MemoryStream());
            servMock.SetupProperty(x => x.Assembly, assembly);

            cfg.Factory.Find.From.Assembly.Using.CurrentAssembly().FindEmbeddedResource(servMock.Object, "dedede", "na");

            servMock.VerifySet(x => x.Assembly = assembly, Times.Once());
        }

        [TestMethod]
        public void when_calling_FindEmbeddedResource_it_should_call_the_FindResource_method_from_the_IAssemblyResourceFinder_once()
        {
            var cfg = new TemplateConfigurator();
            var servMock = new Mock<IAssemblyResourceFinder>();
            var assembly = typeof(IAssemblyResourceFinder).Assembly;

            servMock.SetupAllProperties();
            servMock.Setup(x => x.FindResource<Stream>(It.IsAny<string>()))
                .Returns(new MemoryStream())
                .Verifiable();

            cfg.Factory.Find.From.Assembly.Using.CurrentAssembly().FindEmbeddedResource(servMock.Object, "dedede", "na");

            servMock.Verify(x => x.FindResource<Stream>("dedede"), Times.Once());
        }

        [TestMethod]
        public void when_calling_FindEmbeddedResource_it_should_set_the_Namespace_property_of_the_IAssemblyResourceFinder()
        {
            var cfg = new TemplateConfigurator();
            var servMock = new Mock<IAssemblyResourceFinder>();
            var assembly = typeof(IAssemblyResourceFinder).Assembly;

            servMock.SetupAllProperties();
            servMock.Setup(x => x.FindResource<Stream>(It.IsAny<string>()))
                .Returns(new MemoryStream());

            cfg.Factory.Find.From.Assembly.Using.CurrentAssembly().FindEmbeddedResource(servMock.Object, "dedede", "na");

            servMock.VerifySet(x => x.NamespacePrefix = "na", Times.Once());
        }

        [TestMethod]
        public void if_FindEmbeddedResource_can_not_find_the_embedded_resource_it_should_throw_an_exception()
        {
            var tmpCfg = new TemplateConfigurator();
            var servMock = new Mock<IAssemblyResourceFinder>();

            servMock.SetupAllProperties();
            servMock.Setup(x => x.FindResource<Stream>(TemplateConstants.Solution))
                .Returns<Stream>(null);

            tmpCfg.Invoking(x => x.Factory.Find.From.Assembly.Using.CurrentAssembly()
                    .FindEmbeddedResource(servMock.Object, TemplateConstants.Solution, "na"))
                .ShouldThrow<PostconditionException>();
        }

        [TestMethod]
        public void can_find_an_embedded_template_using_the_TemplateFactory_FindEmbeddedResource_meethod()
        {
            var tmpCfg = new TemplateConfigurator();
            var servMock = new Mock<IAssemblyResourceFinder>();
            var myStream = new MemoryStream();

            servMock.Setup(x => x.FindResource<Stream>("valid"))
                .Returns(myStream);
            servMock.SetupAllProperties();

            Action whenTryingToFindTheEmbeddedTemplateFromTheCurrentAssembly = () =>
                tmpCfg.Factory.Find.From.Assembly.Using.CurrentAssembly()
                    .FindEmbeddedResource(servMock.Object, "valid", "namespace");

            whenTryingToFindTheEmbeddedTemplateFromTheCurrentAssembly.ShouldNotThrow();
            tmpCfg.Factory.EmbeddedTemplate.Should().NotBeNull();
            tmpCfg.Factory.EmbeddedTemplate.Should().Be(myStream);
            tmpCfg.Factory.TemplateFileName.Should().Be("valid");
            tmpCfg.Factory.ResourceNamespace.Should().Be("namespace");
        }

        [TestMethod]
        public void the_FindEmbeddedResource_methos_should_return_the_parent_TemplateConfigurator()
        {
            var tmpCfg = new TemplateConfigurator();
            var servMoq = new Mock<IAssemblyResourceFinder>();

            servMoq.SetupAllProperties();
            servMoq.Setup(x => x.FindResource<Stream>("de"))
                .Returns(new MemoryStream());

            var currentCfg = tmpCfg.Factory.Find.From.Assembly.Using.CurrentAssembly()
                .FindEmbeddedResource(servMoq.Object, "de", "nam");

            currentCfg.Should().NotBeNull().And.Be(tmpCfg);
        }

        #endregion
    }
}
