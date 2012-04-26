// -----------------------------------------------------------------------
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
    using NCastor.AutoBuilder.Console.CodeGenerator.Targets.Build.Versioning;
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
        /// Represents the <see cref="GetBuildNumberTargetGenerator"/> instance used in this class to generate the targets code to get the build number
        /// </summary>
        private GetBuildNumberTargetGenerator getBuildNumberTargetGenerator;

        /// <summary>
        /// Represents the <see cref="GetRevisionVersionTargetGenerator"/> instance used in this class to generate the targets code to get the revision version number
        /// </summary>
        private GetRevisionVersionTargetGenerator getRevisionVersionTargetGenerator;

        /// <summary>
        /// Represents the <see cref="GetAdditionalInformationalVersionTargetGenerator"/> instance used in this class to generate the targets code to get additional information version based on the VCS option
        /// </summary>
        private GetAdditionalInformationalVersionTargetGenerator getAdditionalInformationalVersionTargetGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetsCodeGeneratorController"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="getBuildNumberTargetGenerator">The build number target generator.</param>
        /// <param name="getRevisionVersionTargetGenerator">The get revision version target generator.</param>
        public TargetsCodeGeneratorController(
            CommandLineOptions options,
            GetBuildNumberTargetGenerator getBuildNumberTargetGenerator,
            GetRevisionVersionTargetGenerator getRevisionVersionTargetGenerator,
            GetAdditionalInformationalVersionTargetGenerator getAdditionalInformationalVersionTargetGenerator)
            : base(options)
        {
            Condition.Requires(getBuildNumberTargetGenerator).IsNotNull();
            Condition.Requires(getRevisionVersionTargetGenerator).IsNotNull();
            Condition.Requires(getAdditionalInformationalVersionTargetGenerator).IsNotNull();

            this.getBuildNumberTargetGenerator = getBuildNumberTargetGenerator;
            this.getRevisionVersionTargetGenerator = getRevisionVersionTargetGenerator;
            this.getAdditionalInformationalVersionTargetGenerator = getAdditionalInformationalVersionTargetGenerator;
        }

        /// <summary>
        /// Creates the default versioning targets code.
        /// </summary>
        /// <returns>
        /// Returns the template body
        /// </returns>
        public string CreateDefaultVersioningTargetsCode()
        {
            return this.ProcessTemplate(
                CodeGeneratorTemplateConstants.CommonVersioningCode,
                "NCastor.AutoBuilder.Console.Templates.CodeGenerator.Targets.Build.Versioning",
                (x, y, z) =>
                {
                });
        }

        /// <summary>
        /// Generates the code.
        /// </summary>
        /// <returns>
        /// Returns the template bndy
        /// </returns>
        public override string GenerateCode()
        {
            return this.JoinTargetsCode();
        }

        /// <summary>
        /// Joins the targets code.
        /// </summary>
        /// <returns>
        /// Returns the template bndy
        /// </returns>
        private string JoinTargetsCode()
        {
            StringBuilder finalTargetsCode = new StringBuilder();

            finalTargetsCode.Append(this.CreateDefaultVersioningTargetsCode());
            finalTargetsCode.AppendLine();
            finalTargetsCode.AppendLine();
            finalTargetsCode.Append(this.getBuildNumberTargetGenerator.GenerateCode());
            finalTargetsCode.AppendLine();
            finalTargetsCode.AppendLine();
            finalTargetsCode.Append(this.getRevisionVersionTargetGenerator.GenerateCode());
            finalTargetsCode.AppendLine();
            finalTargetsCode.AppendLine();
            finalTargetsCode.Append(this.getAdditionalInformationalVersionTargetGenerator.GenerateCode());

            return finalTargetsCode.ToString();
        }
    }
}
