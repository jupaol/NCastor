﻿// -----------------------------------------------------------------------
// <copyright file="VcsRunnerPropertiesGeneratorProvider.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using NCastor.AutoBuilder.Console.CodeGenerator.Properties.Runners;
    using Ninject;
    using Ninject.Activation;

    /// <summary>
    /// Ninject provider to create the <see cref="VcsRunnerPropertiesGenerator"/> object
    /// </summary>
    public class VcsRunnerPropertiesGeneratorProvider : IProvider<VcsRunnerPropertiesGenerator>
    {
        /// <summary>
        /// Gets the type (or prototype) of instances the provider creates.
        /// </summary>
        public Type Type
        {
            get { return typeof(VcsRunnerPropertiesGenerator); }
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
            var result = new VcsRunnerPropertiesGenerator(options);

            if (options.VersionControlSystem.HasValue)
            {
                switch (options.VersionControlSystem.Value)
                {
                    case VersionControlSystems.Git:
                        result = new GitRunnerPropertiesGenerator(options);
                        break;
                    case VersionControlSystems.SVN:
                        result = new SvnRunnerPropertiesGenerator(options);
                        break;
                    case VersionControlSystems.TFS:
                        result = new TfsRunnerPropertiesGenerator(options);
                        break;
                }
            }

            return result;
        }
    }
}