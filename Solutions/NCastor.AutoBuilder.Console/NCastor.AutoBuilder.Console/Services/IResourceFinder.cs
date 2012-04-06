// -----------------------------------------------------------------------
// <copyright file="IResourceFinder.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Console.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Service to find a resource
    /// </summary>
    public interface IResourceFinder
    {
        /// <summary>
        /// Finds a resource by name
        /// </summary>
        /// <typeparam name="TResource">The type of the resource.</typeparam>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>
        /// The resource object or throws an InvalidOperationException if the resource is nto found
        /// </returns>
        TResource FindResource<TResource>(string resourceName) where TResource : class;
    }
}
