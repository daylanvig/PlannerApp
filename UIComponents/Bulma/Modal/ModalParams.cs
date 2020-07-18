using Microsoft.AspNetCore.Components;
using System;
using UIComponents.Bulma.Helpers;

namespace UIComponents.Bulma.Modal
{
    public class ModalParams
    {
        public string Title { get; }
        public RenderFragment Body { get; }
        public Type BodyType { get; set; }
        public ModalStyle Style { get; }
        public string SaveLabel { get; }
        public string ModalClass { get; }
        public ModalParams(RenderFragment body, string title = "", ModalStyle style = ModalStyle.Card, string saveLabel = "Save Changes", string modalClass = "")
        {
            Title = title;
            Body = body;
            Style = style;
            SaveLabel = saveLabel;
            ModalClass = modalClass;
        }
        public ModalParams(Type bodyType, string title = "", ModalStyle style = ModalStyle.Card, string saveLabel = "Save Changes", string modalClass = "")
        {
            Title = title;
            Body = new RenderFragment(x => { x.OpenComponent(1, bodyType); x.CloseComponent(); });
            Style = style;
            SaveLabel = saveLabel;
            ModalClass = modalClass;
        }
    }
}
