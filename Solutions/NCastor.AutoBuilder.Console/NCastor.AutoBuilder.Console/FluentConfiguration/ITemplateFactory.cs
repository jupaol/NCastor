// -----------------------------------------------------------------------
// <copyright file="ITemplateFactory.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
using System.Reflection;
using System.Text;
using NCastor.AutoBuilder.Console.Services;

    /// <summary>
    /// This interface represents the factory to get and create a reference to the templates
    /// </summary>
    public interface ITemplateFactory
    {
        /// <summary>
        /// Gets a reference to the current ITemplateFactory object
        /// </summary>
        ITemplateFactory Find { get; }

        /// <summary>
        /// Gets a reference to the current ITemplateFactory object
        /// </summary>
        ITemplateFactory From { get; }

        /// <summary>
        /// Gets a reference to the current ITemplateFactory object
        /// </summary>
        ITemplateFactory Assembly { get; }

        /// <summary>
        /// Gets a reference to the current ITemplateFactory object
        /// </summary>
        ITemplateFactory Using { get; }

        /// <summary>
        /// Gets the embedded template.
        /// </summary>
        /// <remarks>
        /// This member is part of the state of the current template factory process
        /// </remarks>
        Stream EmbeddedTemplate { get; }

        /// <summary>
        /// Gets the name of the template file.
        /// </summary>
        /// <remarks>
        /// This member is part of the state of the current template factory process
        /// </remarks>
        string TemplateFileName { get; }

        /// <summary>
        /// Gets the resource namespace.
        /// </summary>
        /// <remarks>
        /// This member is part of the state of the current template factory process
        /// </remarks>
        string ResourceNamespace { get; }

        /// <summary>
        /// Searches the specified embedded file in the specified assembly
        /// </summary>
        /// <param name="assemblyResourceFinder">The assembly resource finder service.</param>
        /// <param name="embeddedFileName">Name of the embedded file.</param>
        /// <param name="resourceNamespace">The resource namespace.</param>
        /// <returns>
        /// Gets a reference to the parent ITemplateConfigurator object
        /// </returns>
        ITemplateConfigurator FindEmbeddedResource(IAssemblyResourceFinder assemblyResourceFinder, string embeddedFileName, string resourceNamespace);

        /// <summary>
        /// Sets the Curren tAssembly to the Assembly property
        /// </summary>
        /// <returns>
        /// Gets a reference to the current ITemplateFactory object
        /// </returns>
        ITemplateFactory CurrentAssembly();

        /// <summary>
        /// Sets the specified assembly to the Assembly property
        /// </summary>
        /// <param name="assembly">The assembly to be used to search for the embedded file.</param>
        /// <returns>
        /// Gets a reference to the current ITemplateFactory object
        /// </returns>
        ITemplateFactory SpecificAssembly(Assembly assembly);
    }
}
