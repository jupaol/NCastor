# Build help and walkthroughs #

## Build properties ##

-	Configuration

	The configuration used to build your solution.

	Examples:

	```
	<Configuration>Debug</Configuration>
	<Configuration>Release</Configuration>
	```

-	Platform

	The platform used to build your solution

	Examples:

	```
	<Platform>Any CPU</Platform>
	<Platform>Mixed Platforms</Platform>
	<Platform>x86</Platform>
	```

- BuildingPath
- BuildProperties
- SpecialProjectsBuildProperties

## Build targets ##

- ValidateSettings:
- Clean
- CreateArtifactDirectories
- FormatSemanticVersion
- SetAssemblyVersion
- SetAssemblyInfo
- BeforeBuild
- CoreBuild
- AfterBuild
- BuildSpecialProjects

## Walkthroughs ##

- Assembly info
	- Assembly common info
	- Getting the Build version
		- From Hudson
		- From TeamCity
		- From CCNET
		- From TFS
	- Getting the Revision version
		- From Git
		- From SVN
		- From TFS
	- Formatting the Assembly version
	- Formatting the File version
	- Formatting the Informational version
	- Link the `AssemblyCommonInfo.cs` and the `AssemblyVersion.cs` files with each of your projects
- Build the current solution
- Build individual projects

----------

