using PlannerApp.Client.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Client.Components
{
    public class AppState
    {

        private string title = "PlannerApp";
        public string Title 
        { 
            get=> title; 
            set
            {
                title = value;
                NotifyPageChange();
            }
        }

        public event Action OnPageChange;
        
        
        private void NotifyPageChange()
        {
            OnPageChange.Invoke();
        }
    }
}
