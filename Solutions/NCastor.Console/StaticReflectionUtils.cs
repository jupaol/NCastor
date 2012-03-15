// -----------------------------------------------------------------------
// <copyright file="StaticReflectionUtils.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using CuttingEdge.Conditions;

    /// <summary>
    /// Extension methods to get assembly information using reflection
    /// </summary>
    public static class StaticReflectionUtils
    {
        /// <summary>
        /// Gets the copyright information freom the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to find the copyright information</param>
        /// <returns>The copyright information or an empty string if it is not found</returns>
        public static string GetCopyright(this Assembly assembly)
        {
            Condition.Requires(assembly, "assembly").IsNotNull();

            AssemblyCopyrightAttribute objCopyright =
                AssemblyCopyrightAttribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute;

            return objCopyright == null ? string.Empty : objCopyright.Copyright;
        }

        /// <summary>
        /// Gets the assembly title from the specified assembly
        /// </summary>
        /// <param name="assembly">The assembly to find the title information</param>
        /// <returns>The assembly title, or an empty string if the assembly title is not found</returns>
        public static string GetTitle(this Assembly assembly)
        {
            Condition.Requires(assembly, "assembly").IsNotNull();

            AssemblyTitleAttribute objAssemblyTitle =
                AssemblyCopyrightAttribute.GetCustomAttribute(assembly, typeof(AssemblyTitleAttribute)) as AssemblyTitleAttribute;

            return objAssemblyTitle == null ? string.Empty : objAssemblyTitle.Title;
        }

        /// <summary>
        /// Counts the ocurrences found in the source string.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="phrase">The phrase to search and count.</param>
        /// <returns>
        /// The number of ocurrences found in the source string of the phrase specified
        /// </returns>
        public static int CountOcurrences(this string source, string phrase)
        {
            Condition.Requires(source, "source").IsNotNull();
            Condition.Requires(phrase, "phrase").IsNotNull();

            return source
                .Select((c, i) => source.Substring(i))
                .Count(sub => sub.StartsWith(phrase));
        }
    }
}
