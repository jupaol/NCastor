// -----------------------------------------------------------------------
// <copyright file="BootstrapperInitialization.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using System.Reflection;
    using System.Text;
    using Bootstrap;
    using Bootstrap.Locator;
    using Bootstrap.Ninject;

    /// <summary>
    /// Application bootstrapper
    /// </summary>
    public class BootstrapperInitialization
    {
        /// <summary>
        /// Starts the bootstrap process
        /// </summary>
        public void Start()
        {
            Bootstrapper.ClearExtensions();

            Bootstrapper
                .Including
                    .Assembly(Assembly.GetExecutingAssembly())
                .With
                    .Ninject()
                .And
                    .ServiceLocator()
                .Start();
        }
    }
}
