using Microsoft.AspNetCore.Components;
using System;
using UIComponents.Bulma.Helpers;
using UIComponents.Services;

namespace UIComponents.Bulma.Modal
{
    public class ModalBase : UIComponentBase, IDisposable
    {
        [Inject] 
        public IModalService ModalService { get; set; }
        protected ModalStyle Style;
        protected string Title;
        protected RenderFragment Body;
        protected string SaveLabel;
        protected bool IsVisible;
        protected string ModalClass;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ModalService.OnShow += ShowModal;
            ModalService.OnClose += HideModal;
        }

        public void ShowModal(ModalParams modalParams)
        {
            Style = modalParams.Style;
            Title = modalParams.Title;
            Body = modalParams.Body;
            SaveLabel = modalParams.SaveLabel;
            ModalClass = $"modal is-active is-fixed {modalParams.ModalClass}";
            IsVisible = true;
            StateHasChanged();
        }

        public void HideModal()
        {
            IsVisible = false;
            StateHasChanged();
        }

        public void Dispose()
        {
            ModalService.OnShow -= ShowModal;
            ModalService.OnClose -= HideModal;
        }
    }
}
