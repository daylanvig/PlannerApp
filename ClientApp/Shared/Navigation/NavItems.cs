using System.Collections.Generic;

namespace ClientApp.Shared.Navigation
{
    public class NavItems
    {
        public static readonly IEnumerable<NavItem> Items = new List<NavItem>
        {
            new NavItem("/", "Home", "fas fa-home"),
            new NavItem("/Calendar", "Calendar", "fas fa-calendar"),
            new NavItem("/Dashboard", "Dashboard", "fas fa-tachometer-alt")
        };
    }
}
