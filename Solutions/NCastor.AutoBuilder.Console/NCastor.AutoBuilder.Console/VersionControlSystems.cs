// -----------------------------------------------------------------------
// <copyright file="VersionControlSystems.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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

    /// <summary>
    /// The version control systems supported by the application
    /// </summary>
    public enum VersionControlSystems
    {
        /// <summary>
        /// The Git VCS
        /// </summary>
        Git,

        /// <summary>
        /// The SVN VCS
        /// </summary>
        SVN,

        /// <summary>
        /// The TFS VCS
        /// </summary>
        TFS
    }
}
