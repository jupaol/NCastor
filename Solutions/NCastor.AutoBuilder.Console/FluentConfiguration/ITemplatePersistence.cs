// -----------------------------------------------------------------------
// <copyright file="ITemplatePersistence.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// Represents the persistence root object which will be in charge to control the persistence mechanism
    /// for the templates
    /// </summary>
    public interface ITemplatePersistence
    {
        /// <summary>
        /// Gets a reference to the current ITemplatePersistence object.
        /// </summary>
        ITemplatePersistence CurrentTemplate { get; }

        /// <summary>
        /// Gets the output template path.
        /// </summary>
        /// <remarks>
        /// This path is where the current template is saved
        /// </remarks>
        string OutputTemplatePath { get; }

        /// <summary>
        /// Sets the destination file.
        /// </summary>
        /// <returns>
        /// Gets a reference to the current ITemplatePersistence object.
        /// </returns>
        ITemplatePersistence SetDestinationFile();

        /// <summary>
        /// Commits the current template to disk
        /// </summary>
        void Commit();
    }
}
