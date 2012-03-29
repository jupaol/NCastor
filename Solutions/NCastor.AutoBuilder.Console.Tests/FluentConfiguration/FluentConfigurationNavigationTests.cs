using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCastor.AutoBuilder.Console.FluentConfiguration;

namespace NCastor.AutoBuilder.Console.Tests.FluentConfiguration
{
    [TestClass]
    public class FluentConfigurationNavigationTests
    {
        [TestMethod]
        public void can_create_a_new_TemplateConfigurator_instance()
        {
            var templConfig = new TemplateConfigurator();

            templConfig.Should().NotBeNull();
        }

        #region TemplateConfigurator navigation

        [TestMethod]
        public void can_navigate_from_the_TemplateConfigurator_to_the_TemplateFactory_object()
        {
            var tempConfig = new TemplateConfigurator();

            tempConfig.Factory.Should().NotBeNull();
        }

        [TestMethod]
        public void can_navigate_from_Templateconfigurator_to_the_TemplateContext_object()
        {
            var tmpCfg = new TemplateConfigurator();

            tmpCfg.Context.Should().NotBeNull();
        }

        [TestMethod]
        public void can_navigate_from_TemplateConfigurator_to_the_TemplateProcessor_object()
        {
            var tmpCfg = new TemplateConfigurator();

            tmpCfg.Processor.Should().NotBeNull();
        }

        [TestMethod]
        public void can_navigate_from_TemplateConfigurator_to_the_TemplatePersistence_object()
        {
            var tmpCfg = new TemplateConfigurator();

            tmpCfg.Persistence.Should().NotBeNull();
        }

        #endregion

        #region TemplateFactory navigation

        [TestMethod]
        public void can_navigate_from_the_root_configurator_object_to_the_TemplateFactory_Find_member()
        {
            var tmpCfg = new TemplateConfigurator();

            tmpCfg.Factory.Find.Should().NotBeNull();
        }

        [TestMethod]
        public void can_navigate_from_the_root_configurator_object_to_the_TemplateFactory_From_member()
        {
            var tmpCfg = new TemplateConfigurator();

            tmpCfg.Factory.From.Should().NotBeNull();
        }

        [TestMethod]
        public void can_navigate_from_the_root_configurator_obhject_to_the_TemplateFactory_Assembly_member()
        {
            var tmpCfg = new TemplateConfigurator();

            tmpCfg.Factory.Assembly.Should().NotBeNull();
        }

        [TestMethod]
        public void can_navigate_from_the_root_configurator_object_to_the_TemplateFactory_Using_member()
        {
            var tmpCfg = new TemplateConfigurator();

            tmpCfg.Factory.Using.Should().NotBeNull();
        }

        #endregion

        #region TemplateContext navigation

        [TestMethod]
        public void can_navigate_from_the_root_configurator_object_to_the_TemplateContext_Establish_member()
        {
            var tmpCfg = new TemplateConfigurator();

            tmpCfg.Context.Establish.Should().NotBeNull();
        }

        #endregion

        #region TemplateProcessor navigation

        [TestMethod]
        public void can_navigate_from_the_root_configurator_object_to_the_TemplateProcessor_CurrentTemplate_member()
        {
            var tmpCfg = new TemplateConfigurator();

            tmpCfg.Processor.CurrentTemplate.Should().NotBeNull();
        }

        #endregion

        #region TemplatePersistence navigation

        [TestMethod]
        public void can_navigate_from_the_root_configurator_object_to_the_TemplatePersistence_CurrentTemplate_member()
        {
            var tmpCfg = new TemplateConfigurator();

            tmpCfg.Persistence.CurrentTemplate.Should().NotBeNull();
        }

        #endregion
    }
}
