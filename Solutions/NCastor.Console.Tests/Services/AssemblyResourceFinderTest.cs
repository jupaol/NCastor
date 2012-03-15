using NCastor.Console.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;
using System.Reflection;
using System.IO;
using CuttingEdge.Conditions;
using NCastor.Console.Constants;

namespace NCastor.Console.Tests.Services
{
    [TestClass()]
    public class AssemblyResourceFinderTest
    {
        [TestMethod]
        public void can_create_a_new_AssemblyResourceFinder_service()
        {
            var rFinder = new AssemblyResourceFinder();

            rFinder.Should().NotBeNull();
        }

        [TestMethod]
        public void can_get_an_embedded_resource_when_calling_FindResource_from_the_AssemblyResourceFinder_with_a_valid_file_name()
        {
            var finder = new AssemblyResourceFinder();

            finder.Assembly = finder.GetType().Assembly;
            finder.NamespacePrefix = "NCastor.Console.Templates";

            var embeddedResource = finder.FindResource<Stream>(TemplateConstants.Solution);

            embeddedResource.Should().NotBeNull();
        }

        [TestMethod]
        public void when_an_embedded_resource_is_not_found_the_FindResource_from_the_AssemblyResourceFinder_should_throw_an_Exception()
        {
            var finder = new AssemblyResourceFinder();

            finder.Assembly = finder.GetType().Assembly;
            finder.NamespacePrefix = "NCastor.Console.Templatess invalid";

            this.Invoking(x => finder.FindResource<Stream>(TemplateConstants.Solution)).ShouldThrow<PostconditionException>();
        }

        [TestMethod]
        public void calling_FindResource_without_setting_the_assembly_should_throw_an_exception()
        {
            var finder = new AssemblyResourceFinder();

            finder.Invoking(x => finder.FindResource<Stream>(TemplateConstants.Solution))
                .ShouldThrow<ArgumentNullException>()
                .WithMessage("You need to specify the assembly before calling the method FindResource", FluentAssertions.Assertions.ComparisonMode.Substring);
        }

        [TestMethod]
        public void calling_FindResource_with_a_null_resourceName_parameter_it_should_throw_an_exception()
        {
            var finder = new AssemblyResourceFinder();

            finder.Assembly = finder.GetType().Assembly;

            finder.Invoking(x => finder.FindResource<Stream>(null))
                .ShouldThrow<ArgumentException>()
                .WithMessage("resourceName", FluentAssertions.Assertions.ComparisonMode.Substring);
        }
    }
}
