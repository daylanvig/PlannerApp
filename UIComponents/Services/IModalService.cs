using System;
using UIComponents.Bulma.Modal;

namespace UIComponents.Services
{
    public interface IModalService
    {
        event Action OnClose;
        event Action<ModalParams> OnShow;

        void Close();
        void Show(ModalParams modalParams);
    }
}