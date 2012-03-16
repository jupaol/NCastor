// -----------------------------------------------------------------------
// <copyright file="TemplateConstants.cs" company="Juan Pablo Olmos Lara (Jupaol)">
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

namespace NCastor.Console.Constants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// List of avalaible embedded template in the assembly
    /// </summary>
    public static class TemplateConstants
    {
        /// <summary>
        /// Represents the BuildSolution.template file name embedded into the assembly
        /// </summary>
        public const string Solution = "BuildSolution.proj.template";

        /// <summary>
        /// Represents the InitProperties.import.template file name embedded into the assembly
        /// </summary>
        public const string InitProperties = "InitProperties.import.template";

        /// <summary>
        /// Represents the Properties.import.template file name embedded into the assembly
        /// </summary>
        public const string CustomProperties = "Properties.import.template";

        /// <summary>
        /// /// Represents the Targets.import.template file name embedded into the assembly
        /// </summary>
        public const string CustomTargets = "Targets.import.template";

        /// <summary>
        /// Represents the Tasks.import.template file name embedded into the assembly
        /// </summary>
        public const string CustomTasks = "Tasks.import.template";
    }
}
