using AutoFixture;
using AutoFixture.AutoMoq;
using System.Linq;

namespace Shared.TestSupport
{
    public class TestFixture
    {
        public static Fixture Create()
        {
            var fixture = new Fixture();
            AddDefaults(fixture);
            return fixture;
        }

        public static void AddDefaults(Fixture fixture)
        {
            fixture.Customize(new AutoMoqCustomization());
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
