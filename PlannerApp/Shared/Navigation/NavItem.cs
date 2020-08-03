namespace PlannerApp.Client.Shared.Navigation
{
    public class NavItem
    {
        public string Path { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public NavItem(string path, string label, string icon)
        {
            Path = path;
            Icon = icon;
            Label = label;
        }
    }
}
