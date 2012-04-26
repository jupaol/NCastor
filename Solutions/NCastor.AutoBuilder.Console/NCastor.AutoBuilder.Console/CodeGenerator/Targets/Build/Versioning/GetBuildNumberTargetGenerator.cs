// -----------------------------------------------------------------------
// <copyright file="GetBuildNumberTargetGenerator.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Console.CodeGenerator.Targets.Build.Versioning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NCastor.AutoBuilder.Console.Constants;
    using TemplateEngine;

    /// <summary>
    /// Base class to generate the targets needed to get the build number
    /// </summary>
    public class GetBuildNumberTargetGenerator : CodeGeneratorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetBuildNumberTargetGenerator"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public GetBuildNumberTargetGenerator(CommandLineOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Generates the code.
        /// </summary>
        /// <returns>
        /// Returns the template bndy
        /// </returns>
        public override string GenerateCode()
        {
            return this.ProcessTemplate(
                CodeGeneratorTemplateConstants.GenericGetBuildNumberTargetsTemplate,
                "NCastor.AutoBuilder.Console.Templates.CodeGenerator.Targets.Build.Versioning",
                (x, y, z) =>
                {
                });
        }
    }
}
