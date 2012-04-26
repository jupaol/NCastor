// -----------------------------------------------------------------------
// <copyright file="GetRevisionVersionTargetGenerator.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Console.CodeGenerator.BuildTargets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NCastor.AutoBuilder.Console.Constants;

    /// <summary>
    /// Base class to generate MSBuild targets code to get the revision version
    /// </summary>
    public class GetRevisionVersionTargetGenerator : CodeGeneratorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRevisionVersionTargetGenerator"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public GetRevisionVersionTargetGenerator(CommandLineOptions options)
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
                CodeGeneratorTemplateConstants.GenericGetRevisionVersionTargets,
                "NCastor.AutoBuilder.Console.Templates.CodeGenerator.Build.Versioning",
                (x, y, z) =>
                {
                });
        }
    }
}
