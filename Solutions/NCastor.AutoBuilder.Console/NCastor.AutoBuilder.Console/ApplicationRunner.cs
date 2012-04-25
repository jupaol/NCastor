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
        /// Member to store the current application controller
        /// </summary>
        private ApplicationController applicationController;

        /// <summary>
        /// Member to store the current arguments validator
        /// </summary>
        private ArgumentsValidator argumentsValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRunner"/> class.
        /// </summary>
        /// <param name="applicationController">The application controller.</param>
        /// <param name="argumentsValidator">The arguments validator.</param>
        public ApplicationRunner(
            ApplicationController applicationController,
            ArgumentsValidator argumentsValidator)
        {
            this.applicationController = applicationController;
            this.argumentsValidator = argumentsValidator;
        }

        /// <summary>
        /// Occurs when the arguments validation failed.
        /// </summary>
        public event Action<ApplicationController> ArgumentsValidationFailed = delegate { };

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
            try
            {
                if (!this.argumentsValidator.AreArgumentsValid(arguments))
                {
                    this.ArgumentsValidationFailed(this.applicationController);
                }
                else
                {
                    this.applicationController.WithArguments(arguments);

                    this.applicationController.ProcessSolutionTemplate();
                    ////controller.ProcessPropertiesCustomPropertiesTemplate();
                    ////controller.ProcessPropertiesInitPropertiesTemplate();
                    ////controller.ProcessTasksCustomTasksTemplate();
                    ////controller.ProcessTargetsCustomTargetsTemplate();
                    ////controller.ProcessTargetsBuildTargetsTemplate();
                    ////controller.ProcessTargetsRunTestsTargetsTemplate();
                    this.applicationController.ProcessCustomSolutionPropertiesTemplate();
                    this.applicationController.ProcessCustomSolutionTargetsTemplate();
                }
            }
            catch (Exception exc)
            {
                this.ErrorOcurred(exc);
            }
        }
    }
}
