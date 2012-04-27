// -----------------------------------------------------------------------
// <copyright file="VcsRunnerPropertiesGenerator.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Console.CodeGenerator.Properties.Runners
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NCastor.AutoBuilder.Console.Constants;

    /// <summary>
    /// Generates proeprties code for the Version Control System runner paths
    /// </summary>
    public class VcsRunnerPropertiesGenerator : CodeGeneratorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VcsRunnerPropertiesGenerator"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public VcsRunnerPropertiesGenerator(CommandLineOptions options)
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
                CodeGeneratorTemplateConstants.VcsRunnerPropertiesTemplate,
                "NCastor.AutoBuilder.Console.Templates.CodeGenerator.Properties.Runners",
                (x, y, z) =>
                {
                });
        }
    }
}
