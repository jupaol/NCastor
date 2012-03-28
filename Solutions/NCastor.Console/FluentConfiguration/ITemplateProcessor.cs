// -----------------------------------------------------------------------
// <copyright file="ITemplateProcessor.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using System.Linq;
    using System.Text;
using TemplateEngine;

    /// <summary>
    /// Represents the root processor object which will be in charge to process the templates
    /// </summary>
    public interface ITemplateProcessor
    {
        /// <summary>
        /// Gets a reference to the current ITemplateProcessor object
        /// </summary>
        ITemplateProcessor CurrentTemplate { get; }

        /// <summary>
        /// Gets the working template temp path.
        /// </summary>
        /// <remarks>
        /// This member is part of the state of the current template processor process
        /// </remarks>
        string WorkingTemplateTempPath { get; }

        /// <summary>
        /// Gets the final name of the template file.
        /// </summary>
        /// <value>
        /// The final name of the template file.
        /// </value>
        /// <remarks>
        /// This final file name will be used when persisting the current template
        /// </remarks>
        string FinalTemplateFileName { get; }

        /// <summary>
        /// Gets the template body.
        /// </summary>
        string TemplateBody { get; }

        /// <summary>
        /// Prepares the process operation, setting default values for all templates
        /// </summary>
        /// <returns>
        /// Gets a reference to the current ITemplateProcessor object
        /// </returns>
        ITemplateProcessor Prepare();

        /// <summary>
        /// Processes the current template by exposing the Template object to the user.
        /// </summary>
        /// <param name="templateProcessor">The template processor.</param>
        /// <returns>
        /// Gets a reference to the parent ITemplateConfigurator object
        /// </returns>
        ITemplateConfigurator Process(Action<Template, CommandLineOptions, string> templateProcessor);
    }
}
