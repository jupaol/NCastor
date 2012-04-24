using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Microsoft.Practices.ServiceLocation;

namespace NCastor.AutoBuilder.Console.Integration.Tests
{
    [TestClass]
    public class BootstrapperInitializationTests
    {
        [TestClass]
        public class TheStartMethod
        {
            [TestMethod]
            public void it_should_initialize_the_application_using_ninject()
            {
                var sut = new BootstrapperInitialization();

                sut.Invoking(x => x.Start()).ShouldNotThrow();
            }

            [TestMethod]
            public void it_should_initialize_the_ServiceLocator_object()
            {
                var sut = new BootstrapperInitialization();
                IServiceLocator currentLocator = null;

                sut.Start();

                Action callingTheCurrentServiceLocator = () => 
                {
                    currentLocator = ServiceLocator.Current;
                };

                callingTheCurrentServiceLocator.ShouldNotThrow();
                currentLocator.Should().NotBeNull();
            }
        }
    }
}
