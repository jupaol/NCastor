// -----------------------------------------------------------------------
// <copyright file="CodeGenerator.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Console.CodeGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CuttingEdge.Conditions;
    using NCastor.AutoBuilder.Console.FluentConfiguration;
    using NCastor.AutoBuilder.Console.FluentConfiguration.ExtensionMethods;
    using TemplateEngine;

    /// <summary>
    /// Base class to generate code
    /// </summary>
    public abstract class CodeGeneratorBase
    {
        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public CommandLineOptions Options { get; protected set; }

        /// <summary>
        /// Generates the code.
        /// </summary>
        /// <returns>
        /// Returns the template bndy
        /// </returns>
        public abstract string GenerateCode();

        /// <summary>
        /// Processes the template.
        /// </summary>
        /// <param name="embeddedResourceName">Name of the embedded resource.</param>
        /// <param name="resourceNamespace">The resource namespace.</param>
        /// <param name="processingTemplate">The processing template.</param>
        /// <returns>
        /// Returns the template bndy
        /// </returns>
        protected string ProcessTemplate(string embeddedResourceName, string resourceNamespace, Action<Template, CommandLineOptions, string> processingTemplate)
        {
            Condition.Requires(embeddedResourceName, "embeddedResourceName").IsNotNullOrWhiteSpace();
            Condition.Requires(processingTemplate, "processingTemplate").IsNotNull();

            var configurator = new TemplateConfigurator();

            configurator
                .Factory
                    .Find
                    .From
                    .Assembly
                    .Using
                    .CurrentAssembly()
                    .FindEmbeddedResource(embeddedResourceName, resourceNamespace)
                .Context
                    .Establish
                    .Options(this.Options)
                .Processor
                    .CurrentTemplate
                    .Prepare()
                    .Process(processingTemplate);

            return configurator.Processor.TemplateBody;
        }
    }
}
