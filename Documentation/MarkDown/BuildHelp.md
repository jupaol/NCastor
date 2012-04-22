# Build help  #

## Build properties ##

-	**Configuration**

	The configuration used to build your solution. The default value is:

	`<Configuration Condition="$(Configuration) == ''">Debug</Configuration>`

	Examples:

	```
	<Configuration>Debug</Configuration>
	<Configuration>Release</Configuration>
	```
	
-	**Platform**

	The platform used to build your solution. The default value is:

	`<Platform Condition="$(Platform) == ''">Any CPU</Platform>`

	Examples:

	```
	<Platform>Any CPU</Platform>
	<Platform>Mixed Platforms</Platform>
	<Platform>x86</Platform>
	```
	
-	**BuildingPath**

	The path used to build your solution. The default value is:

	`<BuildingPath Condition="$(BuildingPath) == ''">$(WorkingDirectoryPath)\Build\$(Platform)\$(Configuration)</BuildingPath>`

-	**BuildProperties**

	The default MSBuild buil properties. Change this property to set custom build properties. The default value is:

	```
	<BuildProperties>
	  Configuration=$(Configuration);
	  Platform=$(Platform);
	  OutputPath=$(BuildingPath);
	</BuildProperties>
	```
	
	Examples:

	Modifying the build properties to append new configuration values to the default values:

	```
	<BuildProperties>
	  $(BuildProperties);
	  NoLogo=true;
	</BuildProperties>
	```
	
	Overwriting the build properties:

	```
	<BuildProperties>
	  Configuration=MyFixedConfig;
	  Platform=MyFixedPlatform;
	  NoLogo=true;
	</BuildProperties>
	```
		
-	**SpecialProjectsBuildProperties**

	Properties used when building projects individually

	```
	<SpecialProjectsBuildProperties>
	  Configuration=$(Configuration);
	  Platform=$(Platform);
	</SpecialProjectsBuildProperties>
	```

-	**AssemblyVersionFilePath**

	Path to the assembly version file. The default value is:

	`<AssemblyVersionFilePath Condition="$(AssemblyVersionFilePath) == ''">$(AssemblyInfoPath)\AssemblyVersion.cs</AssemblyVersionFilePath>`

	This file will contain the assembly version:

	Example:

	```
	[assembly: AssemblyFileVersion("0.1.0")]
	[assembly: AssemblyInformationalVersion("0.1.0.build.0.git.9.gd940dd93391c")]
	[assembly: AssemblyVersion("0.1.0")]
	```

-	**AssemblyCommonInfoFilePath**

	Path to the assmebly common info file. The default value is:

	`<AssemblyCommonInfoFilePath Condition="$(AssemblyCommonInfoFilePath) == ''">$(AssemblyInfoPath)\AssemblyCommonInfo.cs</AssemblyCommonInfoFilePath>`

	This file will contain the assembly comon info
	
## Build targets ##

-	**ValidateSettings**
-	**Clean**
-	**CreateArtifactDirectories**
-	**FormatSemanticVersion**
-	**SetAssemblyVersion**
-	**SetAssemblyInfo**
-	**BeforeBuild**
-	**CoreBuild**
-	**AfterBuild**
-	**BuildSpecialProjects**

