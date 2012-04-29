# NCastor AutoBuilder #

## Introduction ##

Every serious project today usually needs to be integrated with a Continuous Integration server, so every time a project starts, you have to create custom scripts, configure them to run within the CI server of your choice (Hudson, CCNET, TeamCity, TFS, etc), so usually you copy the MSBuild scripts you just used in the last project and then you start to re-configure them again (hopefully, you did created those scripts to be reusable right?) so you start the development of the new application and you have to start running the scripts to see "if they still work with the new settings", and let’s say you used NUnit in your last project, what happens if now, your team decides that you are not going to use NUnit, maybe in this project you don't have a TestDriven.Net license so you decide to use MSTest because it is already integrated with VisualStudio, well then you just have to learn how to integrate MSTest with your MSBuild scripts to modify your old scripts.... and this is over and over with every project and every technology you want to integrate.

The build process in every project is a boring task, and usually takes more time than estimated funny ha? But on the other hand, having an automated build process since the beginning of the project gives us a big advantage: reduced risk as Martin Fowler explains it:

> The trouble with deferred integration is that it's very hard to predict how long it will take to do, and worse it's very hard to see how far you are through the process. The result is that you are putting yourself into a complete blind spot right at one of tensest parts of a project - even if you're one of the rare cases where you aren't already late.

> Continuous Integration completely finesses this problem. There's no long integration, you completely eliminate the blind spot. At all times you know where you are, what works, what doesn't, the outstanding bugs you have in your system.

> Continuous Integrations doesn't get rid of bugs, but it does make them dramatically easier to find and remove. In this respect it's rather like self-testing code. If you introduce a bug and detect it quickly it's far easier to get rid of.

[Martin Fowler full article](http://martinfowler.com/articles/continuousIntegration.html)

## This is where NCastor AutoBuilder appears to the rescue ##

**NCastor AutoBuilder contains a set of pre-defined MSBuild scripts to allow you quickly INTEGRATE your application with practically any Continuous Integration server that supports MSBuild.**

**NCastor AutoBuilder integrates popular free third party tools** by wrapping them in MSBuild scripts exposing a simple configuration for you. If you follow the established convention, the configuration required is minimal and intuitive and there is a tool to auto generate the scripts with default values for you depending on the tools of your choice, and since NCastor AutoBuilder is composed of MSBuild scripts, you will be able to update them to fit your specific needs. 

You can actually use tools like [MSBuild Explorer](http://www.msbuildexplorer.com/download.htm) to see the whole targets graph used.

[For more information please go to the documentation site](https://github.com/jupaol/NCastor/wiki)
