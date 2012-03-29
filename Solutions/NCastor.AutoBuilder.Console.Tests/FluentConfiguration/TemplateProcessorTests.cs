using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NCastor.AutoBuilder.Console.Constants;
using NCastor.AutoBuilder.Console.FluentConfiguration;
using NCastor.AutoBuilder.Console.Services;

namespace NCastor.AutoBuilder.Console.Tests.FluentConfiguration
{
    [TestClass]
    public class TemplateProcessorTests
    {
        #region Tests for the Prepare method

        [TestMethod]
        public void calling_Prepare_before_assigning_options_it_should_throw_an_ArgumentNullException()
        {
            var cfg = new TemplateConfigurator();
            var ser = new Mock<IAssemblyResourceFinder>();

            ser.SetupAllProperties();
            ser.Setup(x => x.FindResource<Stream>("de"))
                .Returns(new MemoryStream());

            cfg.Invoking(x => x.Factory.Find.From.Assembly.Using.CurrentAssembly()
                    .FindEmbeddedResource(ser.Object, "de", "na")
                    .Processor.CurrentTemplate.Prepare())
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("You need to assign the options before calling the Prepare method", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void calling_Prepare_without_specifying_the_template_to_use_using_the_Factory_it_should_throw_an_ArgumentNullException()
        {
            var cfg = new TemplateConfigurator();

            cfg.Invoking(x => x.Context.Establish.Options(new CommandLineOptions()).Processor.CurrentTemplate.Prepare())
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("You need to assign the template you want to work on before calling the Prepare method", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void calling_Prepare_should_save_the_current_embedded_template_in_a_TMP_folder_assigning_the_full_path_to_the_WorkingTemplateTempPath_property_of_the_ITemplateProcessorh()
        {
            var cfg = this.CreateConfiguration();

            cfg.Processor.CurrentTemplate.Prepare();

            string file1Content;
            string file2Content;

            cfg.Factory.EmbeddedTemplate.Seek(0, SeekOrigin.Begin);
            using (var reader1 = new StreamReader(cfg.Factory.EmbeddedTemplate)) { file1Content = reader1.ReadToEnd(); }
            using (var reader2 = new StreamReader(cfg.Processor.WorkingTemplateTempPath)) { file2Content = reader2.ReadToEnd(); }

            cfg.Processor.WorkingTemplateTempPath.Should().NotBeNullOrEmpty().And.NotBeBlank();
            File.Exists(cfg.Processor.WorkingTemplateTempPath).Should().BeTrue();
            file1Content.Should().Be(file2Content);
        }

        [TestMethod]
        public void calling_Prepare_should_generate_the_FileName_used_by_the_current_template_when_persisted()
        {
            var cfg = this.CreateConfiguration();

            cfg.Processor.CurrentTemplate.Prepare();

            cfg.Processor.FinalTemplateFileName.Should().NotBeNullOrEmpty().And.NotBeBlank()
                .And.Be(string.Format("{0}.{1}",
                    cfg.Context.CurrentOptions.ProductName,
                    cfg.Factory.TemplateFileName.Replace(".template", string.Empty)));
        }

        [TestMethod]
        public void calling_Prepare_should_replace_the_FileName_token_in_the_current_template()
        {
            var cfg = this.CreateConfiguration();

            cfg.Processor.CurrentTemplate.Prepare();

            cfg.Processor.TemplateBody.Should().NotBeNullOrEmpty().And.NotBeBlank();
        }

        [TestMethod]
        public void calling_Prepare_should_return_the_current_ITemplateProcessor_object()
        {
            var cfg = this.CreateConfiguration();

            var currentProcessor = cfg.Processor.CurrentTemplate.Prepare();

            currentProcessor.Should().NotBeNull().And.Be(cfg.Processor);
        }

        #endregion

        #region Tests for the Process method

        [TestMethod]
        public void calling_Process_should_call_the_delegate_PathTooLongException_allow_setting_new_attributes_to_the_current_template()
        {
            var cfg = this.CreateConfiguration();
            bool delegateCalled = false;

            cfg.Processor.CurrentTemplate.Prepare().Process((x, y, z) => delegateCalled = true);

            delegateCalled.Should().BeTrue();
        }

        [TestMethod]
        public void calling_Process_should_expose_the_current_template_engine_to_be_able_to_set_up_custom_attributes_to_the_template()
        {
            var cfg = this.CreateConfiguration();

            cfg.Processor.CurrentTemplate.Prepare().Process((x, y, z) =>
            {
                x.Should().NotBeNull();
                y.Should().NotBeNull();
                z.Should().NotBeNullOrEmpty();
            });
        }

        [TestMethod]
        public void calling_process_should_return_the_parent_ITemplateConfigurator_object()
        {
            var cfg = this.CreateConfiguration();

            var currentCfg = cfg.Processor.CurrentTemplate.Prepare().Process((x, y, z) => { });

            currentCfg.Should().Be(cfg);
        }

        #endregion

        private TemplateConfigurator CreateConfiguration()
        {
            var cfg = new TemplateConfigurator();
            var ser = new Mock<IAssemblyResourceFinder>();
            var res = TemplateConstants.Solution;
            var namespacePrefix = "NCastor.AutoBuilder.Console.Templates";
            var templateBody = "hello '{{ " + TemplateTokenConstants.FileName + " }}' world!";
            var stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(templateBody));
            var opt = new CommandLineOptions { ProductName = "My Prod Name" };

            ser.SetupAllProperties();
            ser.Setup(x => x.FindResource<Stream>(It.IsAny<string>()))
                .Returns(stream);

            cfg
                .Factory
                    .Find.From.Assembly.Using.CurrentAssembly()
                    .FindEmbeddedResource(ser.Object, res, namespacePrefix)
                .Context
                    .Establish
                    .Options(opt);

            return cfg;
        }
    }
}
