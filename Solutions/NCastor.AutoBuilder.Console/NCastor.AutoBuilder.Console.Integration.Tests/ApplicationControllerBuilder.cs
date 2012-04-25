namespace NCastor.AutoBuilder.Console.Integration.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Ploeh.AutoFixture;
    using CommandLine;

    public class ApplicationControllerBuilder
    {
        private List<string> arguments;
        private CommandLineOptions options;
        private TargetsCodeGeneratorController targetsController;

        public ApplicationControllerBuilder()
        {
            this.arguments = new List<string>();
        }

        public ApplicationControllerBuilder WithOptions(CommandLineOptions options)
        {
            this.options = options;
            return this;
        }

        public ApplicationControllerBuilder WithTargetsController(TargetsCodeGeneratorController targetsController)
        {
            this.targetsController = targetsController;
            return this;
        }

        public ApplicationControllerBuilder WithArguments(params string[] arguments)
        {
            this.arguments.AddRange(arguments);
            return this;
        }

        public ApplicationController BuildWithNulls()
        {
            return new ApplicationController(null, null, null, null);
        }

        //public ApplicationController Build()
        //{
        //    return new AutoFixtureHelper().CustomizeFixture(x =>
        //    {
        //        ICommandLineParser parser = new CommandLineParser(new CommandLineParserSettings { MutuallyExclusive = true, CaseSensitive = false });

        //        if (this.wereOptionsSetted)
        //            x.Register<CommandLineOptions>(() => this.options);

        //        if (this.wasTargetControllerSetted)
        //            x.Register<TargetsCodeGeneratorController>(() => this.targetsController);

        //        x.Register<ICommandLineParser>(() => parser);

        //    }).Fixture.CreateAnonymous<ApplicationController>().WithArguments(this.arguments.ToArray());
        //}
    }
}
