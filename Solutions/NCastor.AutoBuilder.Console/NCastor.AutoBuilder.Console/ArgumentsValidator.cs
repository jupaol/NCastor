// -----------------------------------------------------------------------
// <copyright file="ArgumentsValidator.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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

    /// <summary>
    /// Object used to validate the program arguments
    /// </summary>
    public class ArgumentsValidator
    {
        /// <summary>
        /// Represents the parser used to verify if the arguments are valid
        /// </summary>
        private ICommandLineParser parser;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsValidator"/> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        public ArgumentsValidator(ICommandLineParser parser)
        {
            this.parser = parser;
        }

        /// <summary>
        /// Indicates if the arguments valid.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="arguments">The arguments to validate</param>
        /// <returns>
        ///   <c>True</c> if the arguments are valid; otherwise <c>false</c>
        /// </returns>
        public bool AreArgumentsValid(CommandLineOptions options, params string[] arguments)
        {
            bool canParseArguments = this.parser.ParseArguments(arguments, options);

            if (!canParseArguments)
            {
                return false;
            }

            return this.ValidateArguments(options, arguments);
        }

        /// <summary>
        /// Validates the arguments.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        ///   <c>True</c> if the arguments are valid; otherwise <c>false</c>
        /// </returns>
        private bool ValidateArguments(CommandLineOptions options, string[] arguments)
        {
            if (options.GetBuildNumberFrom.HasValue)
            {
                string currentEnumValue = options.GetBuildNumberFrom.Value.ToString();

                return Enum.IsDefined(typeof(ContinuousIntegrationServers), currentEnumValue);
            }

            if (options.VersionControlSystem.HasValue)
            {
                var currentEnumValue = options.VersionControlSystem.Value.ToString();

                return Enum.IsDefined(typeof(VersionControlSystems), currentEnumValue);
            }

            return true;
        }
    }
}
