using MockQueryable.Moq;
using System.Collections.Generic;
using System.Linq;

namespace Shared.TestSupport
{
    public static class MockExtensions
    {
        public static IQueryable<T> AsAsyncQueryable<T>(this IEnumerable<T> enumerable) where T : class
        {
            return enumerable.AsQueryable().BuildMock().Object;
        }
    }
}
