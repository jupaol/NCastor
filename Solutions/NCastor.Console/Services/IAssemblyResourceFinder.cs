// -----------------------------------------------------------------------
// <copyright file="IAssemblyResourceFinder.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.Console.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Service used to find embedded resources in an assembly
    /// </summary>
    public interface IAssemblyResourceFinder : IResourceFinder
    {
        /// <summary>
        /// Gets or sets the assembly to be used to search the embedded resource.
        /// </summary>
        /// <value>
        /// The assembly to be used to look for the resource.
        /// </value>
        Assembly Assembly { get; set; }

        /// <summary>
        /// Gets or sets the namespace prefix used to be appended to the file name specified.
        /// </summary>
        /// <value>
        /// The namespace prefix.
        /// </value>
        string NamespacePrefix { get; set; }
    }
}
