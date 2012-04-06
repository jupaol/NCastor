// -----------------------------------------------------------------------
// <copyright file="ITemplateContext.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// This interface represents the template context root, which will be used to establish and manage the
    /// context used for the current template
    /// </summary>
    public interface ITemplateContext
    {
        /// <summary>
        /// Gets a reference to the current ITemplateContext object
        /// </summary>
        ITemplateContext Establish { get; }

        /// <summary>
        /// Gets the current options used to configure the templates.
        /// </summary>
        /// <remarks>
        /// This member is part of the state of the current template context process
        /// </remarks>
        CommandLineOptions CurrentOptions { get; }

        /// <summary>
        /// Set the options to be used to coonfigure the current template
        /// </summary>
        /// <param name="options">The options to be used to configure the current template.</param>
        /// <returns>
        /// Gets a reference to the parent ITemplateConfigurator object
        /// </returns>
        ITemplateConfigurator Options(CommandLineOptions options);
    }
}
