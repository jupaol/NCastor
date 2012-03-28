// -----------------------------------------------------------------------
// <copyright file="CommandLineOptions.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.Console
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
                "Correct usage:" + Environment.NewLine + "\tNCastor.Console.exe -p <prefix>");
            var help = new HelpText(
                header,
                Assembly.GetExecutingAssembly().GetCopyright(),
                this);

            return help.ToString();
        }
    }
}
