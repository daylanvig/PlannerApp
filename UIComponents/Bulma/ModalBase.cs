using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using UIComponents.Bulma.Helpers;
using UIComponents.Services;

namespace UIComponents.Bulma
{
    public class ModalParams
    {
        public string Title { get; }
        public RenderFragment Body { get; }
        public Type BodyType { get; set; }
        public ModalStyle Style { get; }
        public string SaveLabel { get; }
        public ModalParams(RenderFragment body, string title = "", ModalStyle style = ModalStyle.Card, string saveLabel = "Save Changes")
        {
            Title = title;
            Body = body;
            Style = style;
            SaveLabel = saveLabel;
        }
        public ModalParams(Type bodyType, string title = "", ModalStyle style = ModalStyle.Card, string saveLabel = "Save Changes")
        {
            Title = title;
            Body = new RenderFragment(x => { x.OpenComponent(1, bodyType); x.CloseComponent(); });
            Style = style;
            SaveLabel = saveLabel;
        }
    }

    public class ModalBase : UIComponentBase, IDisposable
    {
        [Inject] 
        public IModalService ModalService { get; set; }
        protected ModalStyle Style;
        protected string Title;
        protected RenderFragment Body;
        protected string SaveLabel;
        protected bool IsVisible;
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
