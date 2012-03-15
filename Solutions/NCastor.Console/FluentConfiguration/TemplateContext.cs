// -----------------------------------------------------------------------
// <copyright file="TemplateContext.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using CuttingEdge.Conditions;

    /// <summary>
    /// This interface represents the template context root, which will be used to establish and manage the
    /// context used for the current template
    /// </summary>
    public class TemplateContext : ITemplateContext
    {
        /// <summary>
        /// Member used to store the parent template configurator object
        /// </summary>
        private ITemplateConfigurator templateConfigurator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateContext"/> class.
        /// </summary>
        /// <param name="templateConfigurator">The parent template configurator.</param>
        public TemplateContext(ITemplateConfigurator templateConfigurator)
        {
            this.templateConfigurator = templateConfigurator;

            this.Establish = this;
        }

        /// <summary>
        /// Gets the current options used to configure the templates.
        /// </summary>
        /// <remarks>
        /// This member is part of the state of the current template context process
        /// </remarks>
        public CommandLineOptions CurrentOptions { get; private set; }

        /// <summary>
        /// Gets a reference to the current ITemplateContext object
        /// </summary>
        public ITemplateContext Establish { get; private set; }

        /// <summary>
        /// Set the options to be used to coonfigure the current template
        /// </summary>
        /// <param name="options">The options to be used to configure the current template.</param>
        /// <returns>
        /// Gets a reference to the parent ITemplateConfigurator object
        /// </returns>
        public ITemplateConfigurator Options(CommandLineOptions options)
        {
            Condition.Requires(options, "options").IsNotNull();

            options.ProcessOptions();
            this.CurrentOptions = options;

            return this.templateConfigurator;
        }
    }
}
