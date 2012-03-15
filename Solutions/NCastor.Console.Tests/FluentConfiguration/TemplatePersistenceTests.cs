using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using NCastor.Console.FluentConfiguration;
using Moq;
using NCastor.Console.Services;
using NCastor.Console.Constants;
using System.IO;

namespace NCastor.Console.Tests.FluentConfiguration
{
    [TestClass]
    public class TemplatePersistenceTests
    {
        #region Tests for the SetDestinationFile method

        [TestMethod]
        public void calling_SetDestinationFile_without_calling_Prepare_from_ITemplateProcessor_should_throw_an_ArgumentException()
        {
            var cfg = this.CreateConfiguration();

            cfg.Invoking(x => x.Persistence.CurrentTemplate.SetDestinationFile())
            .ShouldThrow<ArgumentException>()
            .WithMessage("You need to call at least ITemplateProcessor.Prepare before calling the Commit method", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void caling_SetDestinationFile_should_format_the_file_name_and_store_it_in_the_OutputTemplatePath()
        {
            var cfg = this.CreateFullConfiguration();

            cfg.Persistence.CurrentTemplate.SetDestinationFile();

            cfg.Persistence.OutputTemplatePath.Should().NotBeNullOrEmpty().And.NotBeBlank();
        }

        [TestMethod]
        public void calling_SetDestinationFile_when_the_Resource_namespace_is_null_or_empty_it_should_create_a_path_to_the_file_without_subdirectories_under_the_output_path_specified()
        {
            var cfg = CreateFullConfiguration(null);

            var path = cfg.Persistence.CurrentTemplate.SetDestinationFile().OutputTemplatePath;
            var finalFileName = cfg.Processor.FinalTemplateFileName;

            path.Should().Be(".\\" + finalFileName);
        }

        [TestMethod]
        public void calling_SetDestinationDirectory_when_the_ResourceNamespace_is_the_templates_root_namespace_it_should_create_a_path_directly_under_the_output_directory_specified()
        {
            var cfg = CreateFullConfiguration("NCastor.Console.Templates");

            var path = cfg.Persistence.CurrentTemplate.SetDestinationFile().OutputTemplatePath;
            var finalFileName = cfg.Processor.FinalTemplateFileName;

            path.Should().Be(".\\" + finalFileName);
        }

        [TestMethod]
        public void calling_SetDestinationDirectory_when_the_ResourceNamespace_is_not_null_or_empty_it_should_create_a_path_under_the_output_directory_representing_the_namespace_hierarchy_relative_to_the_Templates_folder_as_subdirectories()
        {
            var cfg = CreateFullConfiguration("NCastor.Console.Templates.Properties.Then.Nested");

            var path = cfg.Persistence.CurrentTemplate.SetDestinationFile().OutputTemplatePath;
            var finalFileName = cfg.Processor.FinalTemplateFileName;
            var outputDir = cfg.Context.CurrentOptions.OutputPath;

            path.Should().Be(".\\Properties\\Then\\Nested\\" + finalFileName);
        }

        [TestMethod]
        public void calling_SetDestinationFile_should_return_the_current_ITemplatePersistence_object()
        {
            var cfg = this.CreateFullConfiguration();

            var currentPersistence = cfg.Persistence.CurrentTemplate.SetDestinationFile();

            currentPersistence.Should().Be(cfg.Persistence);
        }

        #endregion

        #region Tests for the Commit method

        [TestMethod]
        public void calling_Commit_without_calling_SetDestinationFile_it_should_throw_an_Exception()
        {
            var cfg = this.CreateConfiguration();

            cfg.Invoking(x => x.Persistence.CurrentTemplate.Commit())
                .ShouldThrow<ArgumentException>()
                .WithMessage("You need to call SetDestination File before calling the Commit method", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void calling_Commit_should_save_the_current_template_to_disk()
        {
            var cfg = this.CreateFullConfiguration();

            cfg.Persistence.CurrentTemplate.SetDestinationFile().Commit();

            File.Exists(cfg.Persistence.OutputTemplatePath).Should().BeTrue();
        }

        #endregion

        private TemplateConfigurator CreateFullConfiguration(string resourceNamespace = "NCastor.Console.Templates")
        {
            var cfg = this.CreateConfiguration(resourceNamespace);

            cfg.Processor.CurrentTemplate.Prepare().Process((x, y, z) => { });

            return cfg;
        }

        private TemplateConfigurator CreateConfiguration(string resourceNamespace = "NCastor.Console.Templates")
        {
            var cfg = new TemplateConfigurator();
            var ser = new Mock<IAssemblyResourceFinder>();
            var res = TemplateConstants.Solution;
            var namespacePrefix = resourceNamespace;
            var templateBody = "hello '{{ " + TemplateTokenConstants.FileName + " }}' world!";
            var stream = new MemoryStream(ASCIIEncoding.Default.GetBytes(templateBody));
            var opt = new CommandLineOptions { ProductName = "My Prod Name", OutputPath = "." };

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
