// -----------------------------------------------------------------------
// <copyright file="TemplateContext.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using System.Linq;
    using System.Text;
    using CuttingEdge.Conditions;

    /// <summary>
    /// This interface represents the template context root, which will be used to establish and manage the
    /// context used for the current template
    /// </summary>
    public class TemplateContext : ITemplateContext
    {
        /// <summary>
        /// Member used to store the parent template configurator object
        /// </summary>
        private ITemplateConfigurator templateConfigurator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateContext"/> class.
        /// </summary>
        /// <param name="templateConfigurator">The parent template configurator.</param>
        public TemplateContext(ITemplateConfigurator templateConfigurator)
        {
            this.templateConfigurator = templateConfigurator;

            this.Establish = this;
        }

        /// <summary>
        /// Gets the current options used to configure the templates.
        /// </summary>
        /// <remarks>
        /// This member is part of the state of the current template context process
        /// </remarks>
        public CommandLineOptions CurrentOptions { get; private set; }

        /// <summary>
        /// Gets a reference to the current ITemplateContext object
        /// </summary>
        public ITemplateContext Establish { get; private set; }

        /// <summary>
        /// Set the options to be used to coonfigure the current template
        /// </summary>
        /// <param name="options">The options to be used to configure the current template.</param>
        /// <returns>
        /// Gets a reference to the parent ITemplateConfigurator object
        /// </returns>
        public ITemplateConfigurator Options(CommandLineOptions options)
        {
            Condition.Requires(options, "options").IsNotNull();

            options.ProcessOptions();
            this.CurrentOptions = options;

            return this.templateConfigurator;
        }
    }
}
