using AutoFixture;
using AutoFixture.AutoMoq;

namespace Shared.TestSupport
{
    public class TestFixture
    {
        public static Fixture Create()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            return fixture;
        }
    }
}
