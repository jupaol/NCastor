# Create Custom Scripts #

## Generate custom scripts ##

Once you have [installed NCastor AutoBuilder](Install), you need to create custom scripts that will be the bootstrap to access the built-in functionality. 

### Steps to create the custom scripts: ###

- Create an empty Visual Studio Solution in the following path:

`C:\NCABTest`

- Install the NCastor.AutoBuilder.Runner in the solution

`PM> Install-Package NCastor.AutoBuilder.Runner`

The NCastor AutoBuilder should be installed in the following path:

`C:\NCABTest\packages\NCastor.AutoBuilder.Runner.0.1.0`

- Open a console and navigate to: "C:\NCABTest\packages\NCastor.AutoBuilder.Runner.0.1.0\Tools\ConsoleRunner"

`cd "C:\NCABTest\packages\NCastor.AutoBuilder.Runner.0.1.0\Tools\ConsoleRunner"`

- Run the following command: 

`NCastor.AutoBuilder.Console.exe -p MyProductName -o .\..\..\..\..\Build`

Where:

-  **`-p`** indicates the name of your product, this name will be used as a prefix for your custom scripts

-  **`-o`** indicates the output directory where the scripts will be created

This command creates the custom scripts for you in the following location: 

`C:\NCABTest\Build`

These scripts will be used to communicate with NCastor AutoBuilder

Scripts created:

![](images/CustomScripts.png)

- `MyProductName.BuildSolution.proj`: This script is used as the entry point to communicate with NCastor AutoBuilder. (this script will start the build process: `msbuild MyProductName.BuildSolution.proj`)

- `MyProductName.CustomSolution.properties`: This file contains default properties for you. Also you can put in this file your custom properties

- `MyProductName.CustomSolution.targets`: Place in this file your custom targets