// -----------------------------------------------------------------------
// <copyright file="GetAdditionalInformationalVersionTargetGeneratorProvider.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using NCastor.AutoBuilder.Console.CodeGenerator.Targets.Build.Versioning;
    using Ninject;
    using Ninject.Activation;

    /// <summary>
    /// Custom Ninject provider to inject the <see cref="GetAdditionalInformationalVersionTargetGenerator"/> object
    /// </summary>
    public class GetAdditionalInformationalVersionTargetGeneratorProvider : IProvider<GetAdditionalInformationalVersionTargetGenerator>
    {
        /// <summary>
        /// Gets the type (or prototype) of instances the provider creates.
        /// </summary>
        public Type Type
        {
            get { return typeof(GetAdditionalInformationalVersionTargetGenerator); }
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
                        return new GetAdditionalInformationalVersionFromGitTargetGenerator(options);
                    case VersionControlSystems.SVN:
                        return new GetAdditionalInformationalVersionFromSvnTargetGenerator(options);
                    case VersionControlSystems.TFS:
                        return new GetAdditionalInformationalVersionFromTfsTargetGenerator(options);
                    default:
                        return new GetAdditionalInformationalVersionTargetGenerator(options);
                }
            }
            else
            {
                return new GetAdditionalInformationalVersionTargetGenerator(options);
            }
        }
    }
}
