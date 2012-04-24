﻿// -----------------------------------------------------------------------
// <copyright file="TargetsCodeGeneratorController.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CuttingEdge.Conditions;
    using NCastor.AutoBuilder.Console.CodeGenerator;
    using NCastor.AutoBuilder.Console.CodeGenerator.BuildTargets;
    using NCastor.AutoBuilder.Console.Constants;
    using NCastor.AutoBuilder.Console.FluentConfiguration;
    using NCastor.AutoBuilder.Console.FluentConfiguration.ExtensionMethods;
    using TemplateEngine;

    /// <summary>
    /// Code generator used to generate the "targets" related code
    /// </summary>
    /// <remarks>
    /// The generation process is bsaed on the user input
    /// </remarks>
    public class TargetsCodeGeneratorController : CodeGeneratorBase
    {
        /// <summary>
        /// Represents the <see cref="GetBuildNumberTargetGenerator"/> instance used in this class
        /// </summary>
        private GetBuildNumberTargetGenerator getBuildNumberTargetGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetsCodeGeneratorController"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="getBuildNumberTargetGenerator">The get build number target generator.</param>
        public TargetsCodeGeneratorController(
            CommandLineOptions options,
            GetBuildNumberTargetGenerator getBuildNumberTargetGenerator)
        {
            Condition.Requires(options).IsNotNull();

            this.Options = options;
            this.getBuildNumberTargetGenerator = getBuildNumberTargetGenerator;
        }

        /// <summary>
        /// Creates the default versioning targets code.
        /// </summary>
        /// <param name="processingTemplate">The processing template.</param>
        /// <returns>
        /// Returns the template body
        /// </returns>
        public string CreateDefaultVersioningTargetsCode(Action<Template, CommandLineOptions, string> processingTemplate)
        {
            return this.ProcessTemplate(
                CodeGeneratorTemplateConstants.CommonVersioningCode,
                "NCastor.AutoBuilder.Console.Templates.CodeGenerator.Build.Versioning",
                processingTemplate);
        }

        /// <summary>
        /// Generates the code.
        /// </summary>
        /// <returns>
        /// Returns the template bndy
        /// </returns>
        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }
    }
}