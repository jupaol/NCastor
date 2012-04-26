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
        /// Occurs when the arguments validation failed.
        /// </summary>
        public event Action<string> ArgumentsValidationFailed = delegate { };

        /// <summary>
        /// Occurs when error ocurred.
        /// </summary>
        public event Action<Exception> ErrorOcurred = delegate { };

        /// <summary>
        /// Runs the application
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public void Run(string[] arguments)
        {
            new BootstrapperInitialization().Start();
            var argumentsValidator = ServiceLocator.Current.GetInstance<ArgumentsValidator>();

            try
            {
                if (!argumentsValidator.AreArgumentsValid(arguments))
                {
                    this.ArgumentsValidationFailed(argumentsValidator.GetCommandLineHelp());
                }
                else
                {
                    var applicationController = ServiceLocator.Current.GetInstance<ApplicationController>().WithArguments(arguments);

                    applicationController.ProcessSolutionTemplate();
                    ////controller.ProcessPropertiesCustomPropertiesTemplate();
                    ////controller.ProcessPropertiesInitPropertiesTemplate();
                    ////controller.ProcessTasksCustomTasksTemplate();
                    ////controller.ProcessTargetsCustomTargetsTemplate();
                    ////controller.ProcessTargetsBuildTargetsTemplate();
                    ////controller.ProcessTargetsRunTestsTargetsTemplate();
                    applicationController.ProcessCustomSolutionPropertiesTemplate();
                    applicationController.ProcessCustomSolutionTargetsTemplate();
                }
            }
            catch (Exception exc)
            {
                this.ErrorOcurred(exc);
            }
        }
    }
}
