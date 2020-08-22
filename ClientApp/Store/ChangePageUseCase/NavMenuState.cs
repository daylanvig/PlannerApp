using UIComponents.Custom.SheetComponent;

namespace ClientApp.Store.ChangePageUseCase
{
    public class NavMenuState
    {
        public string Title { get; }
        public string SubTitle { get; }
        public SheetParams SheetParams { get; }
        public NavMenuState(string title = "PlannerApp", string subTitle = null, SheetParams sheetParams = null)
        {
            Title = title;
            SubTitle = subTitle;
            SheetParams = sheetParams;
        }
    }
}
