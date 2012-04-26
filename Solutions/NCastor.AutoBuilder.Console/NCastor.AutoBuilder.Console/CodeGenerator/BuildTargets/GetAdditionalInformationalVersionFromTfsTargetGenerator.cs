// -----------------------------------------------------------------------
// <copyright file="GetAdditionalInformationalVersionFromTfsTargetGenerator.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// Generates targets code to get additional informational version from TFS
    /// </summary>
    public class GetAdditionalInformationalVersionFromTfsTargetGenerator : GetAdditionalInformationalVersionTargetGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAdditionalInformationalVersionFromTfsTargetGenerator"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public GetAdditionalInformationalVersionFromTfsTargetGenerator(CommandLineOptions options)
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
                CodeGeneratorTemplateConstants.GetAdditionalInformationalVersionFromTfsTargetsTemplate,
                "NCastor.AutoBuilder.Console.Templates.CodeGenerator.Build.Versioning",
                (x, y, z) =>
                {
                });
        }
    }
}
