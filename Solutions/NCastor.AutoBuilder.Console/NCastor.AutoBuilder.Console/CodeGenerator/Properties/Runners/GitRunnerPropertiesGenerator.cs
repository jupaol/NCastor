// -----------------------------------------------------------------------
// <copyright file="GitRunnerPropertiesGenerator.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// Generates the Git runner properties code
    /// </summary>
    public class GitRunnerPropertiesGenerator : VcsRunnerPropertiesGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GitRunnerPropertiesGenerator"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public GitRunnerPropertiesGenerator(CommandLineOptions options)
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
                CodeGeneratorTemplateConstants.GitRunnerPropertiesTemplate,
                "NCastor.AutoBuilder.Console.Templates.CodeGenerator.Properties.Runners",
                (x, y, z) =>
                {
                });
        }
    }
}
