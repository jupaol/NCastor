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
        /// Member to hold the current command line options
        /// </summary>
        private CommandLineOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsValidator"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="parser">The parser.</param>
        public ArgumentsValidator(CommandLineOptions options, ICommandLineParser parser)
        {
            this.parser = parser;
            this.options = options;
        }

        /// <summary>
        /// Indicates if the arguments valid.
        /// </summary>
        /// <param name="arguments">The arguments to validate</param>
        /// <returns>
        ///   <c>True</c> if the arguments are valid; otherwise <c>false</c>
        /// </returns>
        public bool AreArgumentsValid(params string[] arguments)
        {
            bool canParseArguments = this.parser.ParseArguments(arguments, this.options);

            if (!canParseArguments)
            {
                return false;
            }

            return this.ValidateArguments(this.options, arguments);
        }

        /// <summary>
        /// Gets the command line help.
        /// </summary>
        /// <returns>
        /// The command line help
        /// </returns>
        public string GetCommandLineHelp()
        {
            return this.options.GetHelp();
        }

        /// <summary>
        /// Gets the parsed options.
        /// </summary>
        /// <returns>
        /// Returns the parsed command line options
        /// </returns>
        public CommandLineOptions GetParsedOptions()
        {
            return this.options;
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
            if (options.ContinuousIntegrationServer.HasValue)
            {
                string currentEnumValue = options.ContinuousIntegrationServer.Value.ToString();

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
