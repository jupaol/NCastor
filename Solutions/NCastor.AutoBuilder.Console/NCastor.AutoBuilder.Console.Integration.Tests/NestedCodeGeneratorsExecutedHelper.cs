namespace NCastor.AutoBuilder.Console.Integration.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using FluentAssertions;
    using Ploeh.AutoFixture;
    using Moq;
    using NCastor.AutoBuilder.Console.CodeGenerator;

    internal class NestedCodeGeneratorsExecutedHelper
    {
        private IFixture fixture;

        internal NestedCodeGeneratorsExecutedHelper()
        {
            this.fixture = new AutoFixtureHelper().Fixture;
        }

        internal NestedCodeGeneratorsExecutedHelper CustomizeFixture(Action<IFixture> customizingFixture)
        {
            customizingFixture(this.fixture);
            return this;
        }

        internal void CheckMethodCalled<TMonitored, TSource>(Action<TSource> invokingSutTargetMethod)
            where TMonitored : CodeGeneratorBase
            where TSource : class
        {
            var vcsGeneratorMock = this.fixture.Freeze<Mock<TMonitored>>();

            vcsGeneratorMock.Setup(x => x.GenerateCode()).Returns(It.IsAny<string>()).Verifiable();

            this.fixture.Inject(vcsGeneratorMock.Object);
            var sut = this.fixture.CreateAnonymous<TSource>();

            invokingSutTargetMethod(sut);

            vcsGeneratorMock.Verify(x => x.GenerateCode(), Times.Once());
        }
    }}
