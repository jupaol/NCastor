// -----------------------------------------------------------------------
// <copyright file="TemplateConfigurator.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
