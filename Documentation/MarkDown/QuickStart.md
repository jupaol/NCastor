# Quick Start #

Once you have created your [custom scripts](CreateCustomScripts), you can start using NCastor AutoBuilder.

Now let's add three projects to the NCABTest solution:

- Add a WPF application project named: MyApp.UI

- Add a Class library project named: MyApp.Domain

- Add a Class library project named: MyApp.Queries

- Open the `MyProductName.BuildSolution.proj` file and edit the following properties:

```
<GlobalRootPath>$(MSBuildProjectDirectory)\..</GlobalRootPath>
<SolutionsPath>$(GlobalRootPath)</SolutionsPath>
<SolutionName>NCABTest</SolutionName>
```

Note: when you create the WPF project, Visual Studio creates it by default targeting an x86 platform so we have a solution building several platforms, in order to build the solution we have to instruct NCastor AutoBuilder to use the correct combination of Configuration and Platform

```
<Platform>Mixed Platforms</Platform>
```

**You are done configuring the basics to build your solution!**

Run your build script and check the results:

- Open a command prompt and write the following:

```
cd c:\NCABTest\Build
"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe" MyProductName.BuildSolution.proj
```

![](images/SimpleBuildResult.png)

### What just happened? ###

NCastor AutoBuilder compiled the solution and the results of the compilation are placed in basically three directories:

```
<WorkingDirectoryPath>$(GlobalRootPath)\WorkingDirectory</WorkingDirectoryPath>
<DropsPath>$(GlobalRootPath)\Drops</DropsPath>
<AssemblyInfoPath>$(GlobalRootPath)\CommonAssemblyProperties</AssemblyInfoPath>
```

Which in this case the directories are:

```
C:\NCABTest\WorkingDirectory
C:\NCABTest\Drops
C:\NCABTest\CommonAssemblyProperties
```

- In the `C:\NCABTest\WorkingDirectory` directory we have the following structure containing all the compiled files:

`C:\NCABTest\WorkingDirectory\Build\Mixed Platforms\Debug`

![](images/WorkingDirectoryBuildStructureTree.png)

- In the `C:\NCABTest\Drops` directory, we have two Zip files:

![](images/BasicDroppedFiles.png)

```
NCABTest.Build.Debug.Mixed Platforms.v0.1.0.zip
NCABTest.Source.v0.1.0.zip
```

`NCABTest.Build.Debug.Mixed Platforms.v0.1.0.zip`: contains all the files built

`NCABTest.Source.v0.1.0.zip`: contains all the source code files (from the `$(GlobalRootPath)` directory - `C:\NCABTest`)

- In the `C:\NCABTest\CommonAssemblyProperties` directory we have two files containing the assembly information and the assembly version:

![](images/CommonAssemblyInfoFiles.png)

```
AssemblyCommonInfo.cs
AssemblyVersion.cs
```

These files should be linked to each project to share the assembly info with multiple projects.

```
AssemblyCommonInfo.cs
[assembly: AssemblyTitle("MyProductName")]
[assembly: AssemblyProduct("MyProductName")]
```

```
AssemblyVersion.cs
[assembly: AssemblyFileVersion("0.1.0")]
[assembly: AssemblyInformationalVersion("0.1.0")]
[assembly: AssemblyVersion("0.1.0")]
```

### What's next ###

Read the [NCastor AutoBuilder walkthroughs](Documentation) to learn all the pre-built actions that are ready to be used.