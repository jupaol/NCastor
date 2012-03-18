// -----------------------------------------------------------------------
// <copyright file="CommandLineOptions.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// jupaoljpol@gmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
// 
// * Redistributions of source code must retain the above copyright notice, 
//   this list of conditions and the following disclaimer.
// * Redistributions in binary form must reproduce the above copyright notice, 
//   this list of conditions and the following disclaimer in the documentation 
//   a/nd/or other materials provided with the distribution.
// * Neither the name of the Juan Pablo Olmos Lara (Jupaol) nor the names of its contributors may be 
//   used to endorse or promote products derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED 
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT 
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, 
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
// EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
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
            // To peocess options if needed

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
