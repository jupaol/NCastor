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
    using CommandLine;
    using Microsoft.Practices.ServiceLocation;
    using NCastor.AutoBuilder.Console.CodeGenerator.Targets.Build.Versioning;

    /// <summary>
    /// Application controller factory
    /// </summary>
    public class ApplicationControllerFactory : IApplicationControllerFactory
    {
        /// <summary>
        /// Creates the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        /// A full initialized application controller
        /// </returns>
        public ApplicationController Create(string[] arguments, CommandLineOptions options)
        {
            var controller = ServiceLocator.Current.GetInstance<ApplicationController>();

            controller.WithArguments(arguments);
            this.AddCustomTargetscontroller(controller, options);

            return controller;
        }

        /// <summary>
        /// Adds the custom targetscontroller.
        /// </summary>
        /// <param name="applicationController">The application controller.</param>
        /// <param name="options">The options.</param>
        private void AddCustomTargetscontroller(ApplicationController applicationController, CommandLineOptions options)
        {
            if (options.ContinuousIntegrationServer.HasValue)
            {
                switch (options.ContinuousIntegrationServer.Value)
                {
                    case ContinuousIntegrationServers.Hudson:
                        applicationController.WithCustomTargetsCodeGeneratorController(
                            new TargetsCodeGeneratorController(options, new GetBuildNumberFromHudsonTargetGenerator(options)));
                        break;
                    case ContinuousIntegrationServers.TeamCity:
                        applicationController.WithCustomTargetsCodeGeneratorController(
                            new TargetsCodeGeneratorController(options, new GetBuildNumberFromTeamCityTargetsGenerator(options)));
                        break;
                    case ContinuousIntegrationServers.CCNET:
                        applicationController.WithCustomTargetsCodeGeneratorController(
                            new TargetsCodeGeneratorController(options, new GetBuildNumberFromCcnetTargetsGenerator(options)));
                        break;
                    case ContinuousIntegrationServers.TFS:
                        applicationController.WithCustomTargetsCodeGeneratorController(
                            new TargetsCodeGeneratorController(options, new GetBuildNumberFromTfsTargetsGenerator(options)));
                        break;
                }
            }
        }
    }
}
