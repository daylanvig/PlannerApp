using System;

namespace UIComponents.Services
{
    public interface IApplicationWideComponentService<T>
    {
        event Action OnClose;
        event Action<T> OnShow;

        void Close();
        void Show(T modalParams);
    }
}