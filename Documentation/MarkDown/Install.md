# Install process #

## Installation formats ##

### Nuget format ###

Just install the [Nuget package](https://nuget.org/packages?q=NCastor.AutoBuilder.Runner):

`PM> Install-Package NCastor.AutoBuilder.Runner`

This command will install the NCastor AutoBuilder Runner under the *packages* directory, this Nuget Package is installed at a solution level which means that it won't update any project when installed

Ensure the content is available to your project (usually, inside your VCS)

### Zip format ###

-	[Download](https://github.com/jupaol/NCastor/downloads) the latest release

-	Unzip the file and ensure the content is avalaible to your project (usually, inside your VCS)

## Integrating with a Continuous Integration Server ##

NCastor AutoBuilder can be integrated with practically any CI server.

Note. If you want to use the *build number* used by the CI server, NCastor AutoBuilder has built-in functionality to get this value from the following CI servers. 

-	[Hudson](http://hudson-ci.org/)
-	[TeamCity](http://www.jetbrains.com/teamcity/)
-	[CCNET](http://www.cruisecontrolnet.org/)
-	TFS

If you are using another CI server and if you want to use the *build number* you can still configure NCastor AutoBuilder to use a custom *build number*

## Integrating third party tools (runners) ##

The NCastor AutoBuilder integrates popular third party tools by wrapping them and exposing a simplified configuration for you. NCastor AutoBuilder only needs the path to the tools in order to work which means that you can install the tools at a PC level or include them in the VCS (usually as Nuget packages under the *packages* directory)

## NCastor AutoBuilder supports the following third party tools (runners): ##

### Unit test runners: ###

-	[MSpec (Machine Specifications)](https://github.com/machine/machine.specifications)

	```
	PM> Install-Package Machine.Specifications
	PM> Install-Package Machine.Specifications-Signed
	```
	
-	[MSTest](http://www.microsoft.com/visualstudio/en-us/products/2010-editions/product-comparison)

	You need to install a Visual Studio edition with MSTest support

-	[NUnit](http://www.nunit.org/)

	```
	PM> Install-Package NUnit.Runners
	```
	
### Code coverage runners ###

-	[OpenCover](https://github.com/sawilde/opencover)

	```
	PM> Install-Package OpenCover
	```
	
### Report generators ###

- [ReportGenerator](http://reportgenerator.codeplex.com/)

	```
	PM> Install-Package ReportGenerator
	```
	
### Nuget runners ###

-	[Nuget](https://nuget.org/)

	```
	PM> Install-Package NuGet.CommandLine
	```
	
### Version Control Systems ###

- 	[Git](http://msysgit.github.com/)

	[Guide to install Git ](http://help.github.com/win-set-up-git/)

- 	[SVN](http://tortoisesvn.net/)

- 	[TFS](http://www.microsoft.com/download/en/details.aspx?displaylang=en&id=329)

