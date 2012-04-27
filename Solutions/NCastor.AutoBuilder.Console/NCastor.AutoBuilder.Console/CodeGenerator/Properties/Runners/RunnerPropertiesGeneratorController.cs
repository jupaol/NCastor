// -----------------------------------------------------------------------
// <copyright file="RunnerPropertiesGeneratorController.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// Controller used to generate the runner properties code 
    /// </summary>
    public class RunnerPropertiesGeneratorController : CodeGeneratorBase
    {
        /// <summary>
        /// The version control system runner properties code generator
        /// </summary>
        private VcsRunnerPropertiesGenerator vcsRunnerPropertiesGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RunnerPropertiesGeneratorController"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="vcsRunnerPropertiesGenerator">The VCS runner properties generator.</param>
        public RunnerPropertiesGeneratorController(
            CommandLineOptions options,
            VcsRunnerPropertiesGenerator vcsRunnerPropertiesGenerator)
            : base(options)
        {
            this.vcsRunnerPropertiesGenerator = vcsRunnerPropertiesGenerator;
        }

        /// <summary>
        /// Generates the code.
        /// </summary>
        /// <returns>
        /// Returns the template bndy
        /// </returns>
        public override string GenerateCode()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(this.GetDefaultRunnerPropertiesCode());
            builder.AppendLine();
            builder.AppendLine();
            builder.Append(this.vcsRunnerPropertiesGenerator.GenerateCode());

            return builder.ToString();
        }

        /// <summary>
        /// Gets the default runner properties code.
        /// </summary>
        /// <returns>
        /// Returns the default template bndy
        /// </returns>
        private string GetDefaultRunnerPropertiesCode()
        {
            return this.ProcessTemplate(
                CodeGeneratorTemplateConstants.GenericRunnerPropertiesTemplate,
                "NCastor.AutoBuilder.Console.Templates.CodeGenerator.Properties.Runners",
                (x, y, z) =>
                {
                });
        }
    }
}
