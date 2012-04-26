// -----------------------------------------------------------------------
// <copyright file="GetAdditionalInformationalVersionFromSvnTargetGenerator.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// Generates the targets code to get additional version information from SVN
    /// </summary>
    public class GetAdditionalInformationalVersionFromSvnTargetGenerator : GetAdditionalInformationalVersionTargetGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAdditionalInformationalVersionFromSvnTargetGenerator"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public GetAdditionalInformationalVersionFromSvnTargetGenerator(CommandLineOptions options)
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
                CodeGeneratorTemplateConstants.GetAdditionalInformationalVersionFromSvnTargetsTemplate,
                "NCastor.AutoBuilder.Console.Templates.CodeGenerator.Build.Versioning",
                (x, y, z) =>
                {
                });
        }
    }
}
