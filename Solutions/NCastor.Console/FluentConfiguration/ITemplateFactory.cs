// -----------------------------------------------------------------------
// <copyright file="ITemplateFactory.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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

namespace NCastor.Console.FluentConfiguration
{
    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NCastor.Console.Services;

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
