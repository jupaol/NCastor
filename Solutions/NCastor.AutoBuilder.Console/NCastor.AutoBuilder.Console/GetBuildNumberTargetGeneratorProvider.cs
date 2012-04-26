// -----------------------------------------------------------------------
// <copyright file="GetBuildNumberTargetGeneratorProvider.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using NCastor.AutoBuilder.Console.CodeGenerator.BuildTargets;
    using Ninject;
    using Ninject.Activation;

    /// <summary>
    /// Used to set a custom provider to inject the GetBuildNumberTargetGenerator object
    /// </summary>
    public class GetBuildNumberTargetGeneratorProvider : IProvider<GetBuildNumberTargetGenerator>
    {
        /// <summary>
        /// Gets the type (or prototype) of instances the provider creates.
        /// </summary>
        public Type Type
        {
            get { return typeof(GetBuildNumberTargetGenerator); }
        }

        /// <summary>
        /// Creates an instance within the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The created instance.
        /// </returns>
        public object Create(IContext context)
        {
            var options = context.Kernel.Get<CommandLineOptions>();

            if (options.ContinuousIntegrationServer.HasValue)
            {
                switch (options.ContinuousIntegrationServer.Value)
                {
                    case ContinuousIntegrationServers.Hudson:
                        return new GetBuildNumberFromHudsonTargetGenerator(options);
                    case ContinuousIntegrationServers.TeamCity:
                        return new GetBuildNumberFromTeamCityTargetsGenerator(options);
                    case ContinuousIntegrationServers.CCNET:
                        return new GetBuildNumberFromCcnetTargetsGenerator(options);
                    case ContinuousIntegrationServers.TFS:
                        return new GetBuildNumberFromTfsTargetsGenerator(options);
                    default:
                        return new GetBuildNumberTargetGenerator(options);
                }
            }
            else
            {
                return new GetBuildNumberTargetGenerator(options);
            }
        }
    }
}
