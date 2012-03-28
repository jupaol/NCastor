// -----------------------------------------------------------------------
// <copyright file="AssemblyResourceFinder.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using CuttingEdge.Conditions;

    /// <summary>
    /// Service used to find embedded resources in an assembly
    /// </summary>
    public class AssemblyResourceFinder : IAssemblyResourceFinder
    {
        /// <summary>
        /// Gets or sets the assembly to be used to search the embedded resource.
        /// </summary>
        /// <value>
        /// The assembly to be used to look for the resource.
        /// </value>
        public Assembly Assembly { get; set; }

        /// <summary>
        /// Gets or sets the namespace prefix used to be appended to the file name specified.
        /// </summary>
        /// <value>
        /// The namespace prefix.
        /// </value>
        public string NamespacePrefix { get; set; }

        /// <summary>
        /// Finds a resource by name
        /// </summary>
        /// <typeparam name="TResource">The type of the resource.</typeparam>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>
        /// The resource object
        /// </returns>
        public TResource FindResource<TResource>(string resourceName) where TResource : class
        {
            Condition.Requires(resourceName, "resourceName").IsNotNullOrWhiteSpace();
            Condition.Requires(this.Assembly).IsNotNull("You need to specify the assembly before calling the method FindResource");

            var fullTemplateName = this.FormatResourceFileName(resourceName);
            TResource embeddedResource = this.Assembly.GetManifestResourceStream(fullTemplateName) as TResource;

            Condition.Ensures(embeddedResource).IsNotNull(string.Format(
                "The resource: '{0}' was not found in the assembly: '{1}'", 
                resourceName, 
                this.Assembly.FullName));
            return embeddedResource;
        }

        /// <summary>
        /// Formats the name of the resource file.
        /// </summary>
        /// <param name="resourceFileName">Name of the resource file.</param>
        /// <returns>
        /// The formatted resource file name
        /// </returns>
        /// <remarks>
        /// The format consists in adding the namespace prefix (if specified)
        /// </remarks>
        private string FormatResourceFileName(string resourceFileName)
        {
            string fullTemplateName = this.NamespacePrefix ?? string.Empty;

            fullTemplateName = fullTemplateName.Trim();

            if (!string.IsNullOrWhiteSpace(fullTemplateName))
            {
                if (!fullTemplateName.EndsWith("."))
                {
                    fullTemplateName += ".";
                }
            }

            fullTemplateName += resourceFileName;

            return fullTemplateName;
        }
    }
}
