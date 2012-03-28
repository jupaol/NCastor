// -----------------------------------------------------------------------
// <copyright file="TemplateProcessor.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.Console.FluentConfiguration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using CuttingEdge.Conditions;
    using NCastor.Console.Constants;
    using TemplateEngine;

    /// <summary>
    /// Represents the root processor object which will be in charge to process the templates
    /// </summary>
    public class TemplateProcessor : ITemplateProcessor
    {
        /// <summary>
        /// Member used to store the parent template configurator object
        /// </summary>
        private ITemplateConfigurator templateConfigurator;

        /// <summary>
        /// Member to store the template engine used to process the current template
        /// </summary>
        private Template currentTemplateEngine;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateProcessor"/> class.
        /// </summary>
        /// <param name="templateConfigurator">The parent template configurator.</param>
        public TemplateProcessor(ITemplateConfigurator templateConfigurator)
        {
            this.templateConfigurator = templateConfigurator;

            this.CurrentTemplate = this;
        }

        /// <summary>
        /// Gets a reference to the current ITemplateProcessor object
        /// </summary>
        public ITemplateProcessor CurrentTemplate { get; private set; }

        /// <summary>
        /// Gets the working template temp path.
        /// </summary>
        /// <remarks>
        /// This member is part of the state of the current template processor process
        /// </remarks>
        public string WorkingTemplateTempPath { get; private set; }

        /// <summary>
        /// Gets the final name of the template file.
        /// </summary>
        /// <value>
        /// The final name of the template file.
        /// </value>
        /// <remarks>
        /// This final file name will be used when persisting the current template
        /// </remarks>
        public string FinalTemplateFileName { get; private set; }

        /// <summary>
        /// Gets the template body.
        /// </summary>
        public string TemplateBody
        {
            get { return this.currentTemplateEngine == null ? string.Empty : this.currentTemplateEngine.ToString(); }
        }

        /// <summary>
        /// Prepares the process operation, setting default values for all templates
        /// </summary>
        /// <returns>
        /// Gets a reference to the current ITemplateProcessor object
        /// </returns>
        public ITemplateProcessor Prepare()
        {
            Condition.Requires(this.templateConfigurator.Context.CurrentOptions)
                .IsNotNull("You need to assign the options before calling the Prepare method");
            Condition.Requires(this.templateConfigurator.Factory.EmbeddedTemplate)
                .IsNotNull("You need to assign the template you want to work on before calling the Prepare method");
            Condition.Requires(this.templateConfigurator.Factory.TemplateFileName)
                .IsNotNullOrWhiteSpace("You need to assign the template you want to work on before calling the Prepare method");

            var options = this.templateConfigurator.Context.CurrentOptions;
            var resource = this.templateConfigurator.Factory.EmbeddedTemplate;
            var templateFileName = this.templateConfigurator.Factory.TemplateFileName;

            this.SaveCurrentEmbeddedTemplateToDisk(resource);
            this.GenerateTemplateFileName(templateFileName, options);
            this.ReplaceCommonTokens(options, this.FinalTemplateFileName);

            return this;
        }

        /// <summary>
        /// Processes the current template by exposing the Template object to the user.
        /// </summary>
        /// <param name="templateProcessor">The template processor.</param>
        /// <returns>
        /// Gets a reference to the parent ITemplateConfigurator object
        /// </returns>
        public ITemplateConfigurator Process(Action<Template, CommandLineOptions, string> templateProcessor)
        {
            templateProcessor(this.currentTemplateEngine, this.templateConfigurator.Context.CurrentOptions, this.FinalTemplateFileName);

            return this.templateConfigurator;
        }

        /// <summary>
        /// Replaces the common tokens.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="templateFileName">Name of the template file.</param>
        private void ReplaceCommonTokens(CommandLineOptions options, string templateFileName)
        {
            this.currentTemplateEngine = new Template(this.WorkingTemplateTempPath, false);
        }

        /// <summary>
        /// Generates the name of the template file.
        /// </summary>
        /// <param name="templateFileName">Name of the template file.</param>
        /// <param name="options">The options.</param>
        private void GenerateTemplateFileName(string templateFileName, CommandLineOptions options)
        {
            string newFileName = string.Format(
                "{0}.{1}",
                options.ProductName,
                templateFileName);

            newFileName = newFileName.Replace(".template", string.Empty);

            this.FinalTemplateFileName = newFileName;
        }

        /// <summary>
        /// Saves the current embedded template to disk.
        /// </summary>
        /// <param name="resource">The embedded resource stream to save.</param>
        private void SaveCurrentEmbeddedTemplateToDisk(Stream resource)
        {
            string tmpPath = Path.GetTempFileName();
            var reader = new StreamReader(resource);

            using (var writer = new StreamWriter(tmpPath))
            {
                writer.Write(reader.ReadToEnd());
            }

            this.WorkingTemplateTempPath = tmpPath;
        }
    }
}
