// -----------------------------------------------------------------------
// <copyright file="CodeGeneratorTemplateConstants.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
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
    /// Templates used to auto generate code
    /// </summary>
    public class CodeGeneratorTemplateConstants
    {
        /// <summary>
        /// Represents the 'CommonVersioning.targets.template' template used to generate the common versioning targets code
        /// </summary>
        public const string CommonVersioningCode = "CommonVersioning.targets.template";

        /// <summary>
        /// Represents the 'GetBuildNumberFromHudsonTargets.template' template used to generate the targets to get the build number from Hudson
        /// </summary>
        public const string GetBuildNumberFromHudsonTargetTemplate = "GetBuildNumberFromHudsonTargets.template";

        /// <summary>
        /// Represents the 'GetBuildNumberFromTeamCityTargets.template' template used to generate the targets to get the build number from TeamCity
        /// </summary>
        public const string GetBuildNumberFromTeamCityTargetsTemplate = "GetBuildNumberFromTeamCityTargets.template";

        /// <summary>
        /// Represents the 'GetBuildNumberFromCcnetTargets.template' template used to generate the targets to get the build number from CCNET
        /// </summary>
        public const string GetBuildNumberFromCcnetTargetsTemplate = "GetBuildNumberFromCcnetTargets.template";

        /// <summary>
        /// Represents the 'GetBuildNumberFromTfsTargets.template' template used to generate the targets to get the build number from TFS
        /// </summary>
        public const string GetBuildNumberFromTfsTargetsTemplate = "GetBuildNumberFromTfsTargets.template";
    }
}
