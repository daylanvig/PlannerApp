using System;
using UIComponents.Bulma.Modal;

namespace UIComponents.Services
{
    public class ModalService : IModalService
    {
        public event Action<ModalParams> OnShow;
        public event Action OnClose;

        public void Show(ModalParams modalParams)
        {
            OnShow?.Invoke(modalParams);
        }

        public void Close()
        {
            OnClose?.Invoke();
        }
    }
}
