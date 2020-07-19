using System;

namespace UIComponents.Services
{
    public class ApplicationWideComponentService<T> : IApplicationWideComponentService<T>
    {
        public event Action<T> OnShow;
        public event Action OnClose;

        public void Show(T modalParams)
        {
            OnShow?.Invoke(modalParams);
        }

        public void Close()
        {
            OnClose?.Invoke();
        }
    }
}
