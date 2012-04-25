// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Console
{
    using System;
    using System.IO;
    using Microsoft.Practices.ServiceLocation;
    using NCastor.AutoBuilder.Console.Constants;
    using NCastor.AutoBuilder.Console.FluentConfiguration;

    /// <summary>
    /// Application start point class
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Application start point
        /// </summary>
        /// <param name="args">The arguments</param>
        public static void Main(string[] args)
        {
            new BootstrapperInitialization().Start();
            var controller = ServiceLocator.Current.GetInstance<ApplicationController>().WithArguments(args);

            new ApplicationRunner().Run(args, controller);
        }
    }
}
