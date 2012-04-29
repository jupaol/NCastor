// -----------------------------------------------------------------------
// <copyright file="ApplicationController.cs" company="Juan Pablo Olmos Lara (Jupaol)">
//
// jupaol@hotmail.com
// http://jupaol.blogspot.com/
// 
// </copyright>
// -----------------------------------------------------------------------

namespace NCastor.AutoBuilder.Console
{
    using System;
    using CommandLine;
    using CuttingEdge.Conditions;
    using NCastor.AutoBuilder.Console.CodeGenerator.Properties.Runners;
    using NCastor.AutoBuilder.Console.Constants;
    using NCastor.AutoBuilder.Console.FluentConfiguration;
    using NCastor.AutoBuilder.Console.FluentConfiguration.ExtensionMethods;
    using TemplateEngine;

    /// <summary>
    /// Application controller, used to manage the application flow
    /// </summary>
    public class ApplicationController
    {
        /// <summary>
        /// Member used to store the current application arguments
        /// </summary>
        private string[] arguments;

        /// <summary>
        /// Member used to store the current options (already parsed)
        /// </summary>
        private CommandLineOptions options;

        /// <summary>
        /// Member used to store the current targets code generator controller
        /// </summary>
        private TargetsCodeGeneratorController targetsCodeGeneratorController;

        /// <summary>
        /// Member used to store the current command line parser
        /// </summary>
        private ICommandLineParser parser;

        /// <summary>
        /// Member used to store the current arguments validator
        /// </summary>
        private ArgumentsValidator argumentsValidator;

        /// <summary>
        /// The current controller used to generate the runner properties
        /// </summary>
        private RunnerPropertiesGeneratorController runnerPropertiesGeneratorController;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationController"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="targetsCodeGeneratorController">The targets code generator controller.</param>
        /// <param name="parser">The parser.</param>
        /// <param name="argumentsValidator">The arguments validator.</param>
        /// <param name="runnerPropertiesGeneratorController">The runner properties generator controller.</param>
        public ApplicationController(
            CommandLineOptions options, 
            TargetsCodeGeneratorController targetsCodeGeneratorController,
            ICommandLineParser parser,
            ArgumentsValidator argumentsValidator,
            RunnerPropertiesGeneratorController runnerPropertiesGeneratorController)
        {
            this.options = options;
            this.targetsCodeGeneratorController = targetsCodeGeneratorController;
            this.parser = parser;
            this.argumentsValidator = argumentsValidator;
            this.runnerPropertiesGeneratorController = runnerPropertiesGeneratorController;
        }

        /// <summary>
        /// Sets command line arguments
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        /// A instance of the current <see cref="ApplicationController"/> object
        /// </returns>
        public ApplicationController WithArguments(string[] arguments)
        {
            this.arguments = arguments;

            return this;
        }

        /// <summary>
        /// Processes the template.
        /// </summary>
        /// <param name="embeddedResourceName">Name of the embedded resource.</param>
        /// <param name="resourceNamespace">The resource namespace.</param>
        /// <param name="processingTemplate">The processing template delegate.</param>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessTemplate(string embeddedResourceName, string resourceNamespace, Action<Template, CommandLineOptions, string> processingTemplate)
        {
            Condition.Requires(embeddedResourceName, "embeddedResourceName").IsNotNullOrWhiteSpace();
            Condition.Requires(processingTemplate, "processingTemplate").IsNotNull();
            Condition.Requires(this.AreArgumentsValid())
                .IsTrue("The command line arguments must be valid before calling the ProcessTemplate method");

            var configurator = new TemplateConfigurator();

            configurator
                .Factory
                    .Find
                    .From
                    .Assembly
                    .Using
                    .CurrentAssembly()
                    .FindEmbeddedResource(embeddedResourceName, resourceNamespace)
                .Context
                    .Establish
                    .Options(this.options)
                .Processor
                    .CurrentTemplate
                    .Prepare()
                    .Process(processingTemplate)
                .Persistence
                    .CurrentTemplate
                    .SetDestinationFile()
                    .Commit();

            return configurator;
        }

        /// <summary>
        /// Gets the command line help.
        /// </summary>
        /// <returns>
        /// Returns the command line help
        /// </returns>
        public string GetCommandLineHelp()
        {
            return this.options.GetHelp();
        }

        /// <summary>
        /// Validates if the arguments specified are valid
        /// </summary>
        /// <returns>
        ///   <code>true</code> if the arguments are valid, otherwise <code>false</code>
        /// </returns>
        public bool AreArgumentsValid()
        {
            Condition.Requires(this.arguments).IsNotNull("The arguments specified must not be null");
            
            return this.argumentsValidator.AreArgumentsValid(this.arguments);
        }

        /// <summary>
        /// Processes the solution template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessSolutionTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.Solution, 
                "NCastor.AutoBuilder.Console.Templates",
                (x, y, z) =>
                {
                    x.Set(TemplateTokenConstants.ProductName, y.ProductName);
                    x.Set(TemplateTokenConstants.SolutionName, y.SolutionName);
                });
        }

        /// <summary>
        /// Processes the Custom Properties template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessPropertiesCustomPropertiesTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.CustomProperties,
                "NCastor.AutoBuilder.Console.Templates.Properties",
                (x, y, z) =>
                {
                });
        }

        /// <summary>
        /// Processes Init Properties template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessPropertiesInitPropertiesTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.InitProperties,
                "NCastor.AutoBuilder.Console.Templates.Properties",
                (x, y, z) =>
                {
                    x.Set(TemplateTokenConstants.ProductName, y.ProductName);
                    x.Set(TemplateTokenConstants.SolutionName, y.SolutionName);
                });
        }

        /// <summary>
        /// Processes Custom Tasks template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessTasksCustomTasksTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.CustomTasks,
                "NCastor.AutoBuilder.Console.Templates.Tasks",
                (x, y, z) =>
                {
                });
        }

        /// <summary>
        /// Processes Custom Targets template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessTargetsCustomTargetsTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.CustomTargets,
                "NCastor.AutoBuilder.Console.Templates.Targets",
                (x, y, z) =>
                {
                    x.Set(TemplateTokenConstants.ProductName, y.ProductName);
                });
        }

        /// <summary>
        /// Processes Build Targets template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessTargetsBuildTargetsTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.BuildTargets,
                "NCastor.AutoBuilder.Console.Templates.Targets.Build",
                (x, y, z) =>
                {
                });
        }

        /// <summary>
        /// Processes Run Tests Targets template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessTargetsRunTestsTargetsTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.RunTestsTargets,
                "NCastor.AutoBuilder.Console.Templates.Targets.Tests",
                (x, y, z) =>
                {
                });
        }

        /// <summary>
        /// Processes Versioning Targets template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessTargetsVersioningTargetsTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.VersioningTargets,
                "NCastor.AutoBuilder.Console.Templates.Targets.Build",
                (x, y, z) =>
                {
                });
        }

        /// <summary>
        /// Processes Custom Build Targets template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessTargetsCustomBuildTargetsTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.CustomBuildTargets,
                "NCastor.AutoBuilder.Console.Templates.Targets.Build",
                (x, y, z) =>
                {
                });
        }

        /// <summary>
        /// Processes Custom solution properties template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessCustomSolutionPropertiesTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.CustomSolutionProperties,
                "NCastor.AutoBuilder.Console.Templates",
                (x, y, z) =>
                {
                    x.Set(TemplateTokenConstants.ProductName, y.ProductName);
                    x.Set(CodeGeneratorTemplateTokenConstants.RunnerProperties, this.runnerPropertiesGeneratorController.GenerateCode());
                });
        }

        /// <summary>
        /// Processes Custom solution targets template.
        /// </summary>
        /// <returns>
        /// The TemplateConfigurator object used to process the template
        /// </returns>
        public TemplateConfigurator ProcessCustomSolutionTargetsTemplate()
        {
            return this.ProcessTemplate(
                TemplateConstants.CustomSolutionTargets,
                "NCastor.AutoBuilder.Console.Templates",
                (x, y, z) =>
                {
                    x.Set(CodeGeneratorTemplateTokenConstants.TargetsGeneratedCode, this.targetsCodeGeneratorController.GenerateCode());
                });
        }
    }
}
