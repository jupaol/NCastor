// -----------------------------------------------------------------------
// <copyright file="TemplateFactory.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using NCastor.Console.Services;

    /// <summary>
    /// This interface represents the factory to get and create a reference to the templates
    /// </summary>
    public class TemplateFactory : ITemplateFactory
    {
        /// <summary>
        /// Member used to store the parent template configurator object
        /// </summary>
        private ITemplateConfigurator templateConfigurator;

        /// <summary>
        /// Member to store the working assembly to try to find the resource
        /// </summary>
        private System.Reflection.Assembly workingAssembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateFactory"/> class.
        /// </summary>
        /// <param name="templateConfigurator">The parent template configurator.</param>
        public TemplateFactory(ITemplateConfigurator templateConfigurator)
        {
            this.templateConfigurator = templateConfigurator;

            this.Find = this;
            this.From = this;
            this.Assembly = this;
            this.Using = this;
        }

        /// <summary>
        /// Gets a reference to the current ITemplateFactory object
        /// </summary>
        public ITemplateFactory Find { get; private set; }

        /// <summary>
        /// Gets a reference to the current ITemplateFactory object
        /// </summary>
        public ITemplateFactory From { get; private set; }

        /// <summary>
        /// Gets a reference to the current ITemplateFactory object
        /// </summary>
        public ITemplateFactory Assembly { get; private set; }

        /// <summary>
        /// Gets a reference to the current ITemplateFactory object
        /// </summary>
        public ITemplateFactory Using { get; private set; }

        /// <summary>
        /// Gets the embedded template.
        /// </summary>
        /// <remarks>
        /// This member is part of the state of the current template factory process
        /// </remarks>
        public Stream EmbeddedTemplate { get; private set; }

        /// <summary>
        /// Gets the name of the template file.
        /// </summary>
        /// <value>
        /// The name of the template file.
        /// </value>
        public string TemplateFileName { get; private set; }

        /// <summary>
        /// Gets the resource namespace.
        /// </summary>
        /// <remarks>
        /// This member is part of the state of the current template factory process
        /// </remarks>
        public string ResourceNamespace { get; private set; }

        /// <summary>
        /// Searches the specified embedded file in the specified assembly
        /// </summary>
        /// <param name="assemblyResourceFinder">The assembly resource finder service.</param>
        /// <param name="embeddedFileName">Name of the embedded file.</param>
        /// <param name="resourceNamespace">The resource namespace.</param>
        /// <returns>
        /// Gets a reference to the parent ITemplateConfigurator object
        /// </returns>
        public ITemplateConfigurator FindEmbeddedResource(IAssemblyResourceFinder assemblyResourceFinder, string embeddedFileName, string resourceNamespace)
        {
            Condition.Requires(assemblyResourceFinder, "assemblyResourceFinder").IsNotNull();
            Condition.Requires(embeddedFileName, "embeddedFileName").IsNotNullOrWhiteSpace();
            Condition.Requires(this.workingAssembly).IsNotNull("The working assembly must be assigned before calling FindEmbeddedResource");

            assemblyResourceFinder.Assembly = this.workingAssembly;
            assemblyResourceFinder.NamespacePrefix = resourceNamespace;

            this.EmbeddedTemplate = assemblyResourceFinder.FindResource<Stream>(embeddedFileName);
            this.TemplateFileName = embeddedFileName;
            this.ResourceNamespace = resourceNamespace;

            Condition.Ensures(this.EmbeddedTemplate).IsNotNull(string.Format(
                "The resource: '{0}', was not found in the assembly: '{1}'",
                embeddedFileName,
                assemblyResourceFinder.Assembly.FullName));
            return this.templateConfigurator;
        }

        /// <summary>
        /// Sets the Curren tAssembly to the Assembly property
        /// </summary>
        /// <returns>
        /// Gets a reference to the current ITemplateFactory object
        /// </returns>
        public ITemplateFactory CurrentAssembly()
        {
            this.workingAssembly = this.GetType().Assembly;

            return this;
        }

        /// <summary>
        /// Sets the specified assembly to the Assembly property
        /// </summary>
        /// <param name="assembly">The assembly to be used to search for the embedded file.</param>
        /// <returns>
        /// Gets a reference to the current ITemplateFactory object
        /// </returns>
        public ITemplateFactory SpecificAssembly(System.Reflection.Assembly assembly)
        {
            this.workingAssembly = assembly;

            return this;
        }
    }
}
