// -----------------------------------------------------------------------
// <copyright file="ContinuousIntegrationServers.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// The supported continuous integration servers
    /// </summary>
    public enum ContinuousIntegrationServers
    {
        /// <summary>
        /// The Hudson CIS
        /// </summary>
        Hudson,

        /// <summary>
        /// The TeamCity CIS
        /// </summary>
        TeamCity,

        /// <summary>
        /// The CCNET CIS
        /// </summary>
        CCNET,

        /// <summary>
        /// The TFS CIS
        /// </summary>
        TFS
    }
}
