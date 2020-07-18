using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;

namespace UIComponents.Extensions.TouchSwipe
{
    public class TouchSwipeEvent
    {
        private const int HOLD_TIMEOUT = 250; // ms
        private readonly List<ISwipeEventSubscriber> subscribers = new List<ISwipeEventSubscriber>();
        private DateTime? touchStartTime;
        private double? initialX;
        private double? initialY;

        public void Subscribe(ISwipeEventSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void UnSubscribe(ISwipeEventSubscriber subscriber)
        {
            subscribers.Remove(subscriber);
        }

        private void NotifySubscribers(SwipeDirection direction)
        {
            foreach(var subscriber in subscribers)
            {
                subscriber.HandleSwipe(direction);
            }
        }

        public void HandleTouchStart(TouchEventArgs e)
        {
            touchStartTime = DateTime.Now;
            initialX = e.Touches[0].ClientX;
            initialY = e.Touches[0].ClientY;
        }

        public void HandleTouchMove(TouchEventArgs e)
        {
            // if the value is null, there has already been a move event fired
            if (!touchStartTime.HasValue)
            {
                return;
            }
            if (IsPressAndHold())
            {
                // No action should be taken if the user press and held without moving
                // todo -> debug code
                Console.WriteLine("press and hold");
            }
            else
            {
                SwipeDirection direction;
                var newX = e.Touches[0].ClientX;
                var newY = e.Touches[0].ClientY;
                var diffX = initialX.Value - newX;
                var absDiffX = Math.Abs(diffX);
                var diffY = initialY.Value - newY;
                var absDiffY = Math.Abs(diffY);
                
                // is horizontal swipe
                if (absDiffX > absDiffY)
                {
                    if (absDiffX < 10)
                    {
                        // Reset initial position so slow drags dont count as swipes
                        HandleTouchStart(e);
                        return;
                    }
                    direction = diffX > 0 ? SwipeDirection.Left : SwipeDirection.Right;
                }
                else
                {
                    if(absDiffY < 10)
                    {
                        HandleTouchStart(e);
                        return;
                    }
                    direction = diffY > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                }
                touchStartTime = null;
                initialX = null;
                initialY = null;
                NotifySubscribers(direction);
            }
        }

        private bool IsPressAndHold()
        {
            return (DateTime.Now - touchStartTime.Value).TotalMilliseconds > HOLD_TIMEOUT;
        }
    }
}
