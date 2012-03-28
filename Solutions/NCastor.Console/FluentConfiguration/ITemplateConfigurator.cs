// -----------------------------------------------------------------------
// <copyright file="ITemplateConfigurator.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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

    /// <summary>
    /// This object represents the root fluently configurator
    /// </summary>
    /// <remarks>
    /// This is the root object to start the template configuration process.
    /// This object will be in charge to manage the global configuration state
    /// </remarks>
    public interface ITemplateConfigurator
    {
        /// <summary>
        /// Gets the factory for the current template.
        /// </summary>
        ITemplateFactory Factory { get; }

        /// <summary>
        /// Gets the context for the current template.
        /// </summary>
        ITemplateContext Context { get; }

        /// <summary>
        /// Gets the processor for the current template.
        /// </summary>
        ITemplateProcessor Processor { get; }

        /// <summary>
        /// Gets the persistence for the current template.
        /// </summary>
        ITemplatePersistence Persistence { get; }
    }
}
