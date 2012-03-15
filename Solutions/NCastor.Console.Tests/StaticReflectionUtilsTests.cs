using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using FluentAssertions;

namespace NCastor.Console.Tests
{
    [TestClass]
    public class StaticReflectionUtilsTests
    {
        [TestMethod]
        public void when_calling_the_GetCopyright_extension_method_it_should_return_the_copyright_specified_in_the_assembly()
        {
            Action whenGettingTheCopyrightInfoInTheCurrentAssembly = () => Assembly.GetExecutingAssembly().GetCopyright();

            whenGettingTheCopyrightInfoInTheCurrentAssembly.ShouldNotThrow();
        }

        [TestMethod]
        public void when_calling_GetCopyright_extension_method_from_a_null_aobject_it_should_throw_an_ArgumentNullException()
        {
            Assembly nullAssembly = null;
            Action whenGettingTheCopyrightInfoFromANullObject = () => nullAssembly.GetCopyright();

            whenGettingTheCopyrightInfoFromANullObject.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void when_calling_the_GetTitle_extension_method_it_should_return_the_title_specified_in_the_assembly()
        {
            Action whenGettingTheAssemblyTitleOfTheCurrentAssembly = () => Assembly.GetExecutingAssembly().GetTitle();

            whenGettingTheAssemblyTitleOfTheCurrentAssembly.ShouldNotThrow();
        }

        [TestMethod]
        public void when_calling_the_GetTitle_extension_method_from_a_null_object_it_should_throw_an_ArgumentNullException()
        {
            Assembly nullAssembly = null;
            Action whenGettingTheAssemblyTitleFromANullObject = () => nullAssembly.GetTitle();

            whenGettingTheAssemblyTitleFromANullObject.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void when_calling_CountOcurrences_extension_method_for_avalid_string_it_should_return_the_number_of_ocurrences_of_the_specified_phrase()
        {
            string source = "my name is jj whats your name?, and hers? and your father name?";
            string phrase = "name";

            source.CountOcurrences(phrase).Should().Be(3);
        }

        [TestMethod]
        public void when_calling_the_CountOcurrences_extension_method_from_a_null_object_it_should_throw_an_ArgumentNullException()
        {
            string nullSource = null;
            string phrase = "my phrase";
            Action whenCountingOcurrenciesFromANullObject = () => nullSource.CountOcurrences(phrase);

            whenCountingOcurrenciesFromANullObject.ShouldThrow<ArgumentNullException>();
        }
    }
}
