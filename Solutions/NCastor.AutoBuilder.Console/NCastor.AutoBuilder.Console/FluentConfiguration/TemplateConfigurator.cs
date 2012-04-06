// -----------------------------------------------------------------------
// <copyright file="TemplateConfigurator.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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

    /// <summary>
    /// This object represents the root fluently configurator
    /// </summary>
    /// <remarks>
    /// This is the root object to start the template configuration process.
    /// This object will be in charge to manage the global configuration state
    /// </remarks>
    public class TemplateConfigurator : ITemplateConfigurator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateConfigurator"/> class.
        /// </summary>
        public TemplateConfigurator()
        {
            this.Factory = new TemplateFactory(this);
            this.Context = new TemplateContext(this);
            this.Processor = new TemplateProcessor(this);
            this.Persistence = new TemplatePersistence(this);
        }

        /// <summary>
        /// Gets the factory for the current template.
        /// </summary>
        public ITemplateFactory Factory { get; private set; }

        /// <summary>
        /// Gets the context for the current template.
        /// </summary>
        public ITemplateContext Context { get; private set; }

        /// <summary>
        /// Gets the processor for the current template.
        /// </summary>
        public ITemplateProcessor Processor { get; private set; }

        /// <summary>
        /// Gets the persistence for the current template.
        /// </summary>
        public ITemplatePersistence Persistence { get; private set; }
    }
}
