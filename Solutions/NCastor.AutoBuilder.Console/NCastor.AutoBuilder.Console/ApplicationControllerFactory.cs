// -----------------------------------------------------------------------
// <copyright file="ApplicationControllerFactory.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// Application controller factory
    /// </summary>
    public class ApplicationControllerFactory : IApplicationControllerFactory
    {
        /// <summary>
        /// Creates the specified arguments.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A full initialized application controller
        /// </returns>
        public ApplicationController Create(CommandLineOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
