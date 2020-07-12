namespace PlannerApp.Client.Store.ChangePageUseCase
{
    public class TitleState
    {
        public string Title { get; }
        public string SubTitle { get; }
        public TitleState(string title = "PlannerApp", string subTitle = null)
        {
            Title = title;
            SubTitle = subTitle;
        }
    }
}
