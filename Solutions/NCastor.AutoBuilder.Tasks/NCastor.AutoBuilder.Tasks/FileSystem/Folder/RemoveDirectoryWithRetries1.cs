// -----------------------------------------------------------------------
// <copyright file="RemoveDirectoryWithRetries.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// Copyright (c) 2012, Juan Pablo Olmos Lara (Jupaol)
// All rights reserved.
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Tasks.FileSystem.Folder
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Text;
    using System.Threading;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Tasks;
    using Microsoft.Build.Utilities;

    public class LogMessageFromResourcesDTO
    {
        internal MessageImportance Importance { get; set; }
        internal string MessageResourceName { get; set; }
        internal object[] MessageArgs { get; set; }
    }

    public class LogCommandLineDTO
    {
        internal MessageImportance Importance { get; set; }
        internal string CommandLine { get; set; }
    }

    public class LogErrorWithCodeFromResourcesDTO
    {
        internal string MessageResourceName { get; set; }
        internal object[] MessageArgs { get; set; }
    }

    public class Foo : TaskLoggingHelperFake
    {
        public Foo(ITask taskInstance)
            : base(taskInstance)
        {
        }

        public override bool LogMessageFromText(string lineOfText, MessageImportance messageImportance)
        {
            return false;
        }
    }

    public class TaskLoggingHelperFake : TaskLoggingHelper
    {
        public List<LogMessageFromResourcesDTO> LogMessageFromResourcesArguments { get; set; }
        public List<LogCommandLineDTO> LogCommandLineArguments { get; set; }
        public List<LogErrorWithCodeFromResourcesDTO> LogErrorWithCodeFromResourcesArguments { get; set; }

        public TaskLoggingHelperFake(ITask taskInstance)
            : base(taskInstance)
        {
            this.LogMessageFromResourcesArguments = new List<LogMessageFromResourcesDTO>();
            this.LogCommandLineArguments = new List<LogCommandLineDTO>();
            this.LogErrorWithCodeFromResourcesArguments = new List<LogErrorWithCodeFromResourcesDTO>();
        }

        //public TaskLoggingHelperExtensionFake(ITask taskInstance, ResourceManager primaryResources, ResourceManager sharedResources, string helpKeywordPrefix)
        //    //: base(taskInstance, primaryResources, sharedResources, helpKeywordPrefix)
        //{
        //    this.LogMessageFromResourcesArguments = new List<LogMessageFromResourcesDTO>();
        //    this.LogCommandLineArguments = new List<LogCommandLineDTO>();
        //    this.LogErrorWithCodeFromResourcesArguments = new List<LogErrorWithCodeFromResourcesDTO>();
        //}

        public new void LogMessageFromResources(MessageImportance importance, string messageResourceName, params object[] messageArgs)
        {
            this.LogMessageFromResourcesArguments.Add(new LogMessageFromResourcesDTO
            {
                Importance = importance,
                MessageResourceName = messageResourceName,
                MessageArgs = messageArgs
            });
        }

        public new void LogCommandLine(MessageImportance importance, string commandLine)
        {
            this.LogCommandLineArguments.Add(new LogCommandLineDTO
            {
                Importance = importance,
                CommandLine = commandLine
            });
        }

        public new void LogErrorWithCodeFromResources(string messageResourceName, params object[] messageArgs)
        {
            this.LogErrorWithCodeFromResourcesArguments.Add(new LogErrorWithCodeFromResourcesDTO
            {
                MessageResourceName = messageResourceName,
                MessageArgs = messageArgs
            });
        }

        public virtual bool LogMessageFromText(string lineOfText, MessageImportance messageImportance)
        {
            return true;
        }

        public void ClearMessages()
        {
            this.LogErrorWithCodeFromResourcesArguments.Clear();
            this.LogCommandLineArguments.Clear();
            this.LogMessageFromResourcesArguments.Clear();
        }
    }

    /// <summary>
    /// This is a MSBuild target to remove a directory with retries
    /// </summary>
    /// <remarks>
    /// This class inherits the <see cref="RemoveDir"/> target to allow retries when trying to remove the directory
    /// </remarks>
    public class RemoveDirectoryWithRetries : RemoveDir
    {
        private int numberOfTries = 0;

        private TaskLoggingHelper tmpTaskLoggingHelper;
        private TaskLoggingHelperFake taskLoggingHelperExtensionFake;

        public int RetryCount { get; set; }

        private bool ShouldRetry
        {
            get
            {
                return this.RetryCount > 0;
            }
        }

        public new TaskLoggingHelper Log
        {
            get
            {
                //return this.taskLoggingHelperExtensionFake;
                return new Foo(this);
            }
        }

        public override bool Execute()
        {
            this.tmpTaskLoggingHelper = this.Log;
            //this.taskLoggingHelperExtensionFake = new TaskLoggingHelperExtensionFake(null, null, null, null);
            this.taskLoggingHelperExtensionFake = new TaskLoggingHelperFake(this);

            this.Log.LogMessageFromText("*** Here we go***", MessageImportance.High);

            var obj = this as TaskExtension;

            FieldInfo fieldInfo = typeof(TaskExtension).GetField("logExtension",
                BindingFlags.NonPublic
                | BindingFlags.Instance
                );

            fieldInfo.SetValue(obj, this.taskLoggingHelperExtensionFake);

            base.Log.LogMessageFromText("*** Here we go***", MessageImportance.High);

            if (!this.ShouldRetry)
            {
                return base.Execute();
            }

            bool wasSuccess = false;
            try
            {
                wasSuccess = base.Execute();

                if (wasSuccess)
                {
                    return wasSuccess;
                }
            }
            catch
            {
            }

            return this.RetryTask();
        }

        private bool RetryTask()
        {
            bool wasSuccess = false;

            for (int i = 0; i < this.Directories.Length; i++)
            {
                var item = this.Directories[i];

                if (Directory.Exists(item.ItemSpec))
                {
                    for ( ; this.numberOfTries < this.RetryCount; this.numberOfTries++)
                    {
                        base.Log.LogMessageFromText(
                            string.Format("Sleeping 5 seg... Retry {0} of {1}", this.numberOfTries + 1, this.RetryCount),
                            MessageImportance.High);
                        Thread.Sleep(5000);

                        try
                        {
                            wasSuccess = base.Execute();

                            if (wasSuccess || this.numberOfTries >= this.RetryCount)
                            {
                                return wasSuccess;
                            }
                            else
                            {
                                this.taskLoggingHelperExtensionFake.ClearMessages();
                            }
                        }
                        catch
                        {
                            if (this.numberOfTries >= this.RetryCount)
                            {
                                throw;
                            }
                            else
                            {
                                this.taskLoggingHelperExtensionFake.ClearMessages();
                            }
                        }
                    }
                }
            }

            return wasSuccess;
        }
    }
}
