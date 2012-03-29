// -----------------------------------------------------------------------
// <copyright file="TemplateConstants.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Console.Constants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// List of avalaible embedded template in the assembly
    /// </summary>
    public static class TemplateConstants
    {
        /// <summary>
        /// Represents the BuildSolution template file name embedded into the assembly
        /// </summary>
        public const string Solution = "BuildSolution.proj.template";

        /// <summary>
        /// Represents the VersioningTargets template file name embedded into the assembly
        /// </summary>
        public const string VersioningTargets = "VersioningTargets.import.template";

        /// <summary>
        /// Represents the CustomBuildTargets.template file name embedded into the assembly
        /// </summary>
        public const string CustomBuildTargets = "CustomBuildTargets.import.template";

        /// <summary>
        /// Represents the InitProperties.import.template file name embedded into the assembly
        /// </summary>
        public const string InitProperties = "InitProperties.import.template";

        /// <summary>
        /// Represents the Properties.import.template file name embedded into the assembly
        /// </summary>
        public const string CustomProperties = "Properties.import.template";

        /// <summary>
        /// Represents the Targets.import.template file name embedded into the assembly
        /// </summary>
        public const string CustomTargets = "Targets.import.template";

        /// <summary>
        /// Represents the BuildTargets.import.template file name embedded into the assembly
        /// </summary>
        public const string BuildTargets = "BuildTargets.import.template";

        /// <summary>
        /// Represents the RunTestsTargets.import.template file name embedded into the assembly
        /// </summary>
        public const string RunTestsTargets = "RunTestsTargets.import.template";

        /// <summary>
        /// Represents the Tasks.import.template file name embedded into the assembly
        /// </summary>
        public const string CustomTasks = "Tasks.import.template";
    }
}
