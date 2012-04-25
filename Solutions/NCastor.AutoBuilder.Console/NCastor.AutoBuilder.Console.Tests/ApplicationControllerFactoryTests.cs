using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace NCastor.AutoBuilder.Console.Tests
{
    [TestClass]
    public class ApplicationControllerFactoryTests
    {
        [TestMethod]
        public void can_create_a_new_instance()
        {
            var sut = new ApplicationControllerFactoryHelper().Build();

            sut.Should().NotBeNull();
        }

        [TestClass]
        public class TheCreateMethod
        {
        }
    }

    class ApplicationControllerFactoryHelper
    {
        public ApplicationControllerFactory Build()
        {
            var factory = new ApplicationControllerFactory();

            return factory;
        }
    }
}
