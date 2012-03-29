// -----------------------------------------------------------------------
// <copyright file="StaticReflectionUtils.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Console
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
