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
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Text;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Shared;
    using Microsoft.Build.Tasks;
    using Microsoft.Build.Utilities;
    using System.Threading;

    /// <summary>
    ///               Implements the RemoveDir task. Use the RemoveDir element in your project file to create and execute this task. For usage and parameter information, see RemoveDir Task.
    ///           </summary>
    public class RemoveDirectoryWithRetries : Task
    {
        private ITaskItem[] directories;
        private ITaskItem[] removedDirectories;
        private int numberOfTries = 0;

        /// <summary>
        ///               Gets or sets the directories to delete.
        ///           </summary>
        /// <returns>
        ///               The directories to delete.
        ///           </returns>
        [Required]
        public ITaskItem[] Directories
        {
            get
            {
                return this.directories;
            }
            set
            {
                this.directories = value;
            }
        }

        /// <summary>
        ///               Gets or sets the directories that were successfully deleted.
        ///           </summary>
        /// <returns>
        ///               The directories that were successfully deleted.
        ///           </returns>
        [Output]
        public ITaskItem[] RemovedDirectories
        {
            get
            {
                return this.removedDirectories;
            }
            set
            {
                this.removedDirectories = value;
            }
        }

        public int RetryCount { get; set; }

        /// <summary>
        ///               Executes the RemoveDir task.
        ///           </summary>
        /// <returns>true if task execution succeeded; otherwise, false.
        ///           </returns>
        public override bool Execute()
        {
            bool result = true;
            ArrayList arrayList = new ArrayList();
            ITaskItem[] array = this.Directories;
            for (int i = 0; i < array.Length; i++)
            {
                ITaskItem taskItem = array[i];
                if (Directory.Exists(taskItem.ItemSpec))
                {
                    bool flag = false;
                    ////base.Log.LogMessageFromResources(MessageImportance.Normal, "RemoveDir.Removing", new object[]
                    ////{
                    ////    taskItem.ItemSpec
                    ////});
                    base.Log.LogMessage(MessageImportance.Normal, "Removing directory \"{0}\".", new object[] 
                    {
                        taskItem.ItemSpec 
                    });
                    //base.Log.LogMessageFromResources(MessageImportance.Low, "Shared.ExecCommand", new object[0]);
                    //base.Log.LogCommandLine(MessageImportance.Low, "rd /s /q \"" + taskItem.ItemSpec + "\"");
                    base.Log.LogMessageFromText("rd /s /q \"" + taskItem.ItemSpec + "\"", MessageImportance.Low);
                    bool flag2 = this.RemoveDirectory(taskItem, false, out flag);
                    if (!flag2 && flag)
                    {
                        flag2 = this.RemoveReadOnlyAttributeRecursively(new DirectoryInfo(taskItem.ItemSpec));
                        if (flag2)
                        {
                            flag2 = this.RemoveDirectory(taskItem, true, out flag);
                        }
                    }
                    if (!flag2)
                    {
                        result = false;
                    }
                    if (flag2)
                    {
                        arrayList.Add(new TaskItem(taskItem));
                    }
                }
                else
                {
                    //base.Log.LogMessageFromResources(MessageImportance.Normal, "RemoveDir.SkippingNonexistentDirectory", new object[]
                    //{
                    //    taskItem.ItemSpec
                    //});
                    base.Log.LogMessage(MessageImportance.Normal, "Directory \"{0}\" doesn't exist. Skipping", new object[]
                    {
                        taskItem.ItemSpec
                    });
                    arrayList.Add(new TaskItem(taskItem));
                }
            }
            this.RemovedDirectories = (ITaskItem[])arrayList.ToArray(typeof(ITaskItem));
            return result;
        }

        private bool RemoveDirectory(ITaskItem directory, bool logUnauthorizedError, out bool unauthorizedAccess)
        {
            bool result = true;
            unauthorizedAccess = false;
            try
            {
                Directory.Delete(directory.ItemSpec, true);
            }
            catch (UnauthorizedAccessException ex)
            {
                result = false;
                if (logUnauthorizedError)
                {
                    //base.Log.LogErrorWithCodeFromResources("RemoveDir.Error", new object[]
                    //{
                    //    directory, 
                    //    ex.Message
                    //});
                    base.Log.LogError("MSB3231: Unable to remove directory \"{0}\". {1}", new object[]
					{
						directory, 
						ex.Message
					});
                }
                unauthorizedAccess = true;
            }
            //updated from original code
            catch (IOException ioException)
            {
                base.Log.LogMessage("Exception occurred while removing directory \"{0}\". {1}", new object[] { directory, ioException.Message });
                if (this.numberOfTries < this.RetryCount)
                {
                    this.numberOfTries++;
                    this.Log.LogMessage(string.Format("***Waiting 5 seg to retry again. Attempt {0} of {1}", this.numberOfTries, this.RetryCount));
                    Thread.Sleep(5000);
                    RemoveDirectory(directory, logUnauthorizedError, out unauthorizedAccess);
                }
                else
                {
                    result = false;
                }
            }
            //end update
            catch (Exception ex2)
            {
                RethrowUnlessFileIO(ex2);
                //base.Log.LogErrorWithCodeFromResources("RemoveDir.Error", new object[]
                //{
                //    directory.ItemSpec, 
                //    ex2.Message
                //});
                base.Log.LogError("MSB3231: Unable to remove directory \"{0}\". {1}", new object[]
				{
					directory.ItemSpec, 
					ex2.Message
				});
                result = false;
            }
            return result;
        }

        private bool RemoveReadOnlyAttributeRecursively(DirectoryInfo directory)
        {
            bool result = true;
            try
            {
                if ((directory.Attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
                {
                    FileAttributes attributes = directory.Attributes & ~FileAttributes.ReadOnly;
                    directory.Attributes = attributes;
                }
                FileSystemInfo[] fileSystemInfos = directory.GetFileSystemInfos();
                for (int i = 0; i < fileSystemInfos.Length; i++)
                {
                    FileSystemInfo fileSystemInfo = fileSystemInfos[i];
                    if ((fileSystemInfo.Attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
                    {
                        FileAttributes attributes2 = fileSystemInfo.Attributes & ~FileAttributes.ReadOnly;
                        fileSystemInfo.Attributes = attributes2;
                    }
                }
                DirectoryInfo[] array = directory.GetDirectories();
                for (int j = 0; j < array.Length; j++)
                {
                    DirectoryInfo directory2 = array[j];
                    result = this.RemoveReadOnlyAttributeRecursively(directory2);
                }
            }
            catch (Exception ex)
            {
                RethrowUnlessFileIO(ex);
                //base.Log.LogErrorWithCodeFromResources("RemoveDir.Error", new object[]
                //{
                //    directory, 
                //    ex.Message
                //});
                base.Log.LogError("MSB3231: Unable to remove directory \"{0}\". {1}", new object[]
				{
					directory, 
					ex.Message
				});
                result = false;
            }
            return result;
        }

        internal static void RethrowUnlessFileIO(Exception e)
        {
            if (e is UnauthorizedAccessException || e is ArgumentNullException || e is PathTooLongException || e is DirectoryNotFoundException || e is NotSupportedException || e is ArgumentException || e is SecurityException || e is IOException)
            {
                return;
            }
            throw e;
        }
    }
}
