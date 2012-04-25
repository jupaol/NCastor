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
            var runner = new ApplicationRunner();

            runner.ArgumentsValidationFailed += (x) => 
            {
                Console.WriteLine(x);
                Environment.Exit(1);
            };

            runner.ErrorOcurred += (exc) =>
            {
                Console.Error.WriteLine(exc.Message);
                Environment.Exit(1);
            };

            runner.Run(args);
        }
    }
}
