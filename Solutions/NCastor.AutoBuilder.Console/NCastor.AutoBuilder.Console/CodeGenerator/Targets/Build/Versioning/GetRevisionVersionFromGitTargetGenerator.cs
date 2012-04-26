// -----------------------------------------------------------------------
// <copyright file="GetRevisionVersionFromGitTargetGenerator.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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

    /// <summary>
    /// Generates the targets code to get the revision version from Git
    /// </summary>
    public class GetRevisionVersionFromGitTargetGenerator : GetRevisionVersionTargetGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRevisionVersionFromGitTargetGenerator"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public GetRevisionVersionFromGitTargetGenerator(CommandLineOptions options)
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
                CodeGeneratorTemplateConstants.GetRevisionVersionFromGitTargetsTemplate,
                "NCastor.AutoBuilder.Console.Templates.CodeGenerator.Targets.Build.Versioning",
                (x, y, z) =>
                {
                });
        }
    }
}
