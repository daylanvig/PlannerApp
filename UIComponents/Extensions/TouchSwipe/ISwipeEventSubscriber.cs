using System.Threading.Tasks;

namespace UIComponents.Extensions.TouchSwipe
{
    public interface ISwipeEventSubscriber
    {
        Task HandleSwipe(SwipeDirection direction);
    }
}
