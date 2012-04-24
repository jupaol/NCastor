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

namespace NCastor.AutoBuilder.Console.CodeGenerator.BuildTargets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
        {
            this.Options = options;
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
