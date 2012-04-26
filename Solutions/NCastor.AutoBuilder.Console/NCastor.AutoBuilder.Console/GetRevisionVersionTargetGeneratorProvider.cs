// -----------------------------------------------------------------------
// <copyright file="GetRevisionVersionTargetGeneratorProvider.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    /// Custom provider to inject the <see cref="GetRevisionVersionTargetGenerator"/> type
    /// </summary>
    public class GetRevisionVersionTargetGeneratorProvider : IProvider<GetRevisionVersionTargetGenerator>
    {
        /// <summary>
        /// Gets the type (or prototype) of instances the provider creates.
        /// </summary>
        public Type Type
        {
            get { return typeof(GetRevisionVersionTargetGenerator); }
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

            if (options.VersionControlSystem.HasValue)
            {
                switch (options.VersionControlSystem.Value)
                {
                    case VersionControlSystems.Git:
                        return new GetRevisionVersionFromGitTargetGenerator(options);
                    case VersionControlSystems.SVN:
                        return new GetRevisionVersionFromSvnTargetGenerator(options);
                    case VersionControlSystems.TFS:
                        return new GetRevisionVersionFromTfsTargetGenerator(options);
                    default:
                        return new GetRevisionVersionTargetGenerator(options);
                }
            }
            else
            {
                return new GetRevisionVersionTargetGenerator(options);
            }
        }
    }
}
