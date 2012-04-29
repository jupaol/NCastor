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

namespace NCastor.AutoBuilder.Console.NinjectProviders
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
            var result = new GetAdditionalInformationalVersionTargetGenerator(options);

            if (options.VersionControlSystem.HasValue)
            {
                switch (options.VersionControlSystem.Value)
                {
                    case VersionControlSystems.Git:
                        result = new GetAdditionalInformationalVersionFromGitTargetGenerator(options);
                        break;
                    case VersionControlSystems.SVN:
                        result = new GetAdditionalInformationalVersionFromSvnTargetGenerator(options);
                        break;
                    case VersionControlSystems.TFS:
                        result = new GetAdditionalInformationalVersionFromTfsTargetGenerator(options);
                        break;
                }
            }

            return result;
        }
    }
}
