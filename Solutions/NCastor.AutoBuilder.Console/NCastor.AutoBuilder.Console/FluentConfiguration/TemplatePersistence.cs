// -----------------------------------------------------------------------
// <copyright file="TemplatePersistence.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Console.FluentConfiguration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using CuttingEdge.Conditions;

    /// <summary>
    /// Represents the persistence root object which will be in charge to control the persistence mechanism
    /// for the templates
    /// </summary>
    public class TemplatePersistence : ITemplatePersistence
    {
        /// <summary>
        /// Member used to store the parent template configurator object
        /// </summary>
        private ITemplateConfigurator templateConfigurator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplatePersistence"/> class.
        /// </summary>
        /// <param name="templateConfigurator">The parent template configurator.</param>
        public TemplatePersistence(ITemplateConfigurator templateConfigurator)
        {
            this.templateConfigurator = templateConfigurator;

            this.CurrentTemplate = this;
        }

        /// <summary>
        /// Gets a reference to the current ITemplatePersistence object.
        /// </summary>
        public ITemplatePersistence CurrentTemplate { get; private set; }

        /// <summary>
        /// Gets the output template path.
        /// </summary>
        /// <remarks>
        /// This path is where the current template is saved
        /// </remarks>
        public string OutputTemplatePath { get; private set; }

        /// <summary>
        /// Sets the destination file.
        /// </summary>
        /// <returns>
        /// Gets a reference to the current ITemplatePersistence object.
        /// </returns>
        public ITemplatePersistence SetDestinationFile()
        {
            Condition.Requires(this.templateConfigurator.Processor.TemplateBody)
            .IsNotNullOrWhiteSpace("You need to call at least ITemplateProcessor.Prepare before calling the Commit method");

            var options = this.templateConfigurator.Context.CurrentOptions;
            var templateFileName = this.templateConfigurator.Processor.FinalTemplateFileName;
            var resourceNamespace = this.templateConfigurator.Factory.ResourceNamespace;

            this.OutputTemplatePath = this.CalculateOutputPath(options, templateFileName, resourceNamespace);

            return this;
        }

        /// <summary>
        /// Commits the current template to disk
        /// </summary>
        public void Commit()
        {
            Condition.Requires(this.OutputTemplatePath)
                .IsNotNullOrWhiteSpace("You need to call SetDestination File before calling the Commit method");

            var templateBody = this.templateConfigurator.Processor.TemplateBody;

            this.SaveFile(this.OutputTemplatePath, templateBody);
        }

        /// <summary>
        /// Calculates the output path.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="templateFileName">Name of the template file.</param>
        /// <param name="resourceNamespace">The resource namespace.</param>
        /// <returns>
        /// Returns the full path for the template. This path will be used when saving the template
        /// </returns>
        private string CalculateOutputPath(CommandLineOptions options, string templateFileName, string resourceNamespace)
        {
            var path = string.Empty;

            if (string.IsNullOrWhiteSpace(resourceNamespace))
            {
                path = Path.Combine(options.OutputPath, templateFileName);
            }
            else
            {
                var rootNamespace = options.GetType().Namespace + ".Templates";
                var customNamespace = resourceNamespace
                    .Replace(rootNamespace, string.Empty)
                    .Trim()
                    .TrimStart('.')
                    .Replace(".", Path.DirectorySeparatorChar.ToString());

                path = Path.Combine(options.OutputPath, Path.Combine(customNamespace, templateFileName));
            }

            return path;
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="templatePath">The template path.</param>
        /// <param name="templateBody">The template body.</param>
        private void SaveFile(string templatePath, string templateBody)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(templatePath));

            if (!directory.Exists)
            {
                directory.Create();
            }

            using (var writer = new StreamWriter(templatePath))
            {
                writer.Write(templateBody);
            }

            this.OutputTemplatePath = templatePath;
        }
    }
}
