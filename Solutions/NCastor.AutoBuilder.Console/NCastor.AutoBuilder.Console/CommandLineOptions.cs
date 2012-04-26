// -----------------------------------------------------------------------
// <copyright file="CommandLineOptions.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Console
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using CommandLine;
    using CommandLine.Text;
    using CuttingEdge.Conditions;

    /// <summary>
    /// Represents the command line options allowed for the application
    /// </summary>
    public class CommandLineOptions
    {
        /// <summary>
        /// The product name argument used to name all the generated script files
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = JustificationForTheFieldsMustBePrivate)]
        [Option("p", null, HelpText = "-p <ProductName> your product name", Required = true)]
        public string ProductName;

        /// <summary>
        /// The output path to place the auto generated scripts
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = JustificationForTheFieldsMustBePrivate)]
        [Option("o", null, HelpText = @"-o <OutputPath> output path to place the scripts, accepts relative path (usually: ..\..\..\..\Build)", Required = true)]
        public string OutputPath;

        /// <summary>
        /// The solution name
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = JustificationForTheFieldsMustBePrivate)]
        [Option("s", null, HelpText = "[-s <SolutionName>] your solution name", Required = false)]
        public string SolutionName;

        /// <summary>
        /// Specifies the source to get the build number
        /// </summary>
        /// <remarks>
        /// This option is used to generate custom targets to get the build number from the CIS specified
        /// </remarks>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = JustificationForTheFieldsMustBePrivate)]
        [Option(null, "CIS", HelpText = "[--CIS <Hudson | TeamCity | CCNET | TFS>] the CIS source to get the build number. This option is used to generate custom targets to get the build number", Required = false)]
        public ContinuousIntegrationServers? ContinuousIntegrationServer;

        /// <summary>
        /// Specifies the version control system used
        /// </summary>
        /// <remarks>
        /// This option is used to generate custom targets to use the version control system specified
        /// </remarks>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = JustificationForTheFieldsMustBePrivate)]
        [Option(null, "VCS", HelpText = "[--VCS <Git | SVN | TFS>] the VCS used in the application. This option is used to generate custom targets using this VCS", Required = false)]
        public VersionControlSystems? VersionControlSystem;

        /// <summary>
        /// Justification for the supress message attributes
        /// </summary>
        private const string JustificationForTheFieldsMustBePrivate = "Required by the Command Line Parser framework";

        /// <summary>
        /// Processes the options.
        /// </summary>
        public void ProcessOptions()
        {
            //// To peocess options if needed

            if (this.SolutionName == null)
            {
                this.SolutionName = string.Empty;
            }
        }

        /// <summary>
        /// Gets the apslication help indicating the correct usage of the application
        /// </summary>
        /// <returns>The default help message for the application</returns>
        [HelpOption("?", "help", HelpText = "Displays this help text")]
        public string GetHelp()
        {
            var header = string.Format(
                "{0}{1}{2}",
                Assembly.GetExecutingAssembly().GetTitle(),
                Environment.NewLine,
                "Correct usage:" + Environment.NewLine + "\tNCastor.AutoBuilder.Console.exe -p <prefix>");
            var help = new HelpText(
                header,
                Assembly.GetExecutingAssembly().GetCopyright(),
                this);

            return help.ToString();
        }
    }
}
