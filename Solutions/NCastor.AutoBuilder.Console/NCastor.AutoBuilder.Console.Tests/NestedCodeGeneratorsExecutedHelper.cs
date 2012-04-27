namespace NCastor.AutoBuilder.Console.Tests
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
        internal void CheckMethodCalled<TMonitored, TSource>()
            where TMonitored : CodeGeneratorBase
            where TSource : CodeGeneratorBase
        {
            var fixture = new AutoFixtureHelper().Fixture;
            var vcsGeneratorMock = fixture.Freeze<Mock<TMonitored>>();

            vcsGeneratorMock.Setup(x => x.GenerateCode()).Returns(It.IsAny<string>()).Verifiable();

            fixture.Inject(vcsGeneratorMock.Object);
            var sut = fixture.CreateAnonymous<TSource>();

            sut.GenerateCode();

            vcsGeneratorMock.Verify(x => x.GenerateCode(), Times.Once());
        }
    }}
