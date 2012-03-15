// -----------------------------------------------------------------------
// <copyright file="AssemblyResourceFinder.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// jupaoljpol@gmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer.
// * Redistributions in binary form must reproduce the above copyright notice, 
//   this list of conditions and the following disclaimer in the documentation 
//   a/nd/or other materials provided with the distribution.
// * Neither the name of the Juan Pablo Olmos Lara (Jupaol) nor the names of its contributors may be 
//   used to endorse or promote products derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED 
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT 
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, 
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
// EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
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
