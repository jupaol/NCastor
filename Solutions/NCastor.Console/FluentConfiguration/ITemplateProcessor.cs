// -----------------------------------------------------------------------
// <copyright file="ITemplateProcessor.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using System.Linq;
    using System.Text;
using TemplateEngine;

    /// <summary>
    /// Represents the root processor object which will be in charge to process the templates
    /// </summary>
    public interface ITemplateProcessor
    {
        /// <summary>
        /// Gets a reference to the current ITemplateProcessor object
        /// </summary>
        ITemplateProcessor CurrentTemplate { get; }

        /// <summary>
        /// Gets the working template temp path.
        /// </summary>
        /// <remarks>
        /// This member is part of the state of the current template processor process
        /// </remarks>
        string WorkingTemplateTempPath { get; }

        /// <summary>
        /// Gets the final name of the template file.
        /// </summary>
        /// <value>
        /// The final name of the template file.
        /// </value>
        /// <remarks>
        /// This final file name will be used when persisting the current template
        /// </remarks>
        string FinalTemplateFileName { get; }

        /// <summary>
        /// Gets the template body.
        /// </summary>
        string TemplateBody { get; }

        /// <summary>
        /// Prepares the process operation, setting default values for all templates
        /// </summary>
        /// <returns>
        /// Gets a reference to the current ITemplateProcessor object
        /// </returns>
        ITemplateProcessor Prepare();

        /// <summary>
        /// Processes the current template by exposing the Template object to the user.
        /// </summary>
        /// <param name="templateProcessor">The template processor.</param>
        /// <returns>
        /// Gets a reference to the parent ITemplateConfigurator object
        /// </returns>
        ITemplateConfigurator Process(Action<Template, CommandLineOptions, string> templateProcessor);
    }
}
