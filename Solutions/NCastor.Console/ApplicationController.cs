// -----------------------------------------------------------------------
// <copyright file="ApplicationController.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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

namespace NCastor.Console
{
    using System;
    using CommandLine;
    using CuttingEdge.Conditions;
    using NCastor.Console.Constants;
    using NCastor.Console.FluentConfiguration;
    using NCastor.Console.FluentConfiguration.ExtensionMethods;
    using TemplateEngine;

    /// <summary>
    /// Application controller, used to manage the application flow
    /// </summary>
    public class ApplicationController
    {
        /// <summary>
        /// Member used to store the current application arguments
        /// </summary>
        private string[] arguments;

        /// <summary>
        /// Member used to store the current options (already parsed)
        /// </summary>
        private CommandLineOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationController"/> class.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public ApplicationController(string[] arguments)
        {
            this.arguments = arguments;

            this.options = new CommandLineOptions();
        }

        /// <summary>
        /// Processes the template.
        /// </summary>
        /// <param name="embeddedResourceName">Name of the embedded resource.</param>
        /// <param name="resourceNamespace">The resource namespace.</param>
        /// <param name="processingTemplate">The processing template delegate.</param>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessTemplate(string embeddedResourceName, string resourceNamespace, Action<Template, CommandLineOptions, string> processingTemplate)
        {
            Condition.Requires(embeddedResourceName, "embeddedResourceName").IsNotNullOrWhiteSpace();
            Condition.Requires(processingTemplate, "processingTemplate").IsNotNull();
            Condition.Requires(this.AreArgumentsValid())
                .IsTrue("The command line arguments must be valid before calling the ProcessTemplate method");

            var configurator = new TemplateConfigurator();

            configurator
                .Factory
                    .Find
                    .From
                    .Assembly
                    .Using
                    .CurrentAssembly()
                    .FindEmbeddedResource(embeddedResourceName, resourceNamespace)
                .Context
                    .Establish
                    .Options(this.options)
                .Processor
                    .CurrentTemplate
                    .Prepare()
                    .Process(processingTemplate)
                .Persistence
                    .CurrentTemplate
                    .SetDestinationFile()
                    .Commit();

            return configurator;
        }

        /// <summary>
        /// Gets the command line help.
        /// </summary>
        /// <returns>
        /// Returns the command line help
        /// </returns>
        public string GetCommandLineHelp()
        {
            return this.options.GetHelp();
        }

        /// <summary>
        /// Validates if the arguments specified are valid
        /// </summary>
        /// <returns>
        ///   <code>true</code> if the arguments are valid, otherwise <code>false</code>
        /// </returns>
        public bool AreArgumentsValid()
        {
            Condition.Requires(this.arguments).IsNotNull("The arguments specified must not be null");

            return new CommandLineParser().ParseArguments(this.arguments, this.options);
        }

        /// <summary>
        /// Processes the solution template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessSolutionTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.Solution, 
                "NCastor.Console.Templates",
                (x, y, z) =>
                {
                    x.Set(TemplateTokenConstants.ProductName, y.ProductName);
                });
        }

        /// <summary>
        /// Processes the Properties.Custom.Properties template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessPropertiesCustomPropertiesTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.CustomProperties,
                "NCastor.Console.Templates.Properties",
                (x, y, z) =>
                {
                });
        }

        /// <summary>
        /// Processes the Properties.Init.Properties template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessPropertiesInitPropertiesTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.InitProperties,
                "NCastor.Console.Templates.Properties",
                (x, y, z) =>
                {
                    x.Set(TemplateTokenConstants.ProductName, y.ProductName);
                    x.Set(TemplateTokenConstants.SolutionName, y.SolutionName);
                });
        }
    }
}
