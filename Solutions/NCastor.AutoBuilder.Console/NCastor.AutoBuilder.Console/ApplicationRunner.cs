// -----------------------------------------------------------------------
// <copyright file="ApplicationRunner.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// Start point of the application
    /// </summary>
    public class ApplicationRunner
    {
        /// <summary>
        /// Runs the application
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public void Run(string[] arguments)
        {
            new BootstrapperInitialization().Start();

            var controller = ServiceLocator.Current.GetInstance<ApplicationController>().WithArguments(arguments);

            if (!controller.AreArgumentsValid())
            {
                Console.WriteLine(controller.GetCommandLineHelp());
                Environment.Exit(1);
            }

            try
            {
                controller.ProcessSolutionTemplate();
                ////controller.ProcessPropertiesCustomPropertiesTemplate();
                ////controller.ProcessPropertiesInitPropertiesTemplate();
                ////controller.ProcessTasksCustomTasksTemplate();
                ////controller.ProcessTargetsCustomTargetsTemplate();
                ////controller.ProcessTargetsBuildTargetsTemplate();
                ////controller.ProcessTargetsRunTestsTargetsTemplate();
                controller.ProcessCustomSolutionPropertiesTemplate();
                controller.ProcessCustomSolutionTargetsTemplate();
            }
            catch (Exception exc)
            {
                Console.Error.WriteLine(exc.Message);
                Environment.Exit(1);
            }

            Environment.Exit(0);
        }
    }
}
