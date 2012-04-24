
namespace NCastor.AutoBuilder.Console.Integration.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    public class AutoFixtureHelper
    {
        public IFixture Fixture;

        public AutoFixtureHelper()
        {
            this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        public AutoFixtureHelper CustomizeFixture(Action<IFixture> customizingFixtureDelegate)
        {
            customizingFixtureDelegate(this.Fixture);
            return this;
        }
    }
}
