using ChartJs.Blazor.ChartJS.Common.Handlers.OnClickHandler;
using ChartJs.Blazor.ChartJS.Common.Properties;
using ChartJs.Blazor.ChartJS.PieChart;
using ChartJs.Blazor.Charts;
using ChartJs.Blazor.ChartJS.Common.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using PresentationClient.Services;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UIComponents.Bulma.Helpers;
using UIComponents.Bulma.Modal;
using UIComponents.Services;
using Newtonsoft.Json.Linq;

namespace PresentationClient.Components.DashboardComponents
{
    public class TimeSpentByCategoryReportBase : ComponentBase
    {
        [Inject] ICategoryService CategoryService { get; set; }
        [Inject] IPlannerItemDataService PlannerItemDataService { get; set; }
        [Inject] IApplicationWideComponentService<ModalParams> ModalService { get; set; }

        protected IDictionary<CategoryDTO, int> CategoryTimes;
        protected int MaxTime;
        protected ChartJsPieChart chart;
        protected override async Task OnInitializedAsync()
        {
            CategoryTimes = await CategoryService.GetTotalMinutesByCategory();
            MaxTime = CategoryTimes.Any() ? CategoryTimes.Max(c => c.Value) : 0;
        }

        protected string CalculatePercentTotalWidth(int itemTime)
        {
            if (MaxTime == 0)
            {
                return "0%";
            }
            var relativeToMax = (double)itemTime / MaxTime;
            return $"{Math.Truncate(relativeToMax * 100)}%";
        }

        [JSInvokable]
        public void ClickItem(object sender, object args)
        {
            // TODO: I think this will have to be implemented from js rather than from asp
            var test = (JsonElement)args;
        }

        protected PieConfig GetChartConfig()
        {
            var labels = new List<string>();
            var values = new List<double>();
            var colours = new List<string>();

            foreach (var category in CategoryTimes)
            {
                labels.Add(category.Key.Description);
                values.Add(category.Value);
                colours.Add(category.Key.Colour);
            }
            var config = new PieConfig
            {
                Options = new PieOptions
                {
                    Responsive = true,
                    Animation = new ArcAnimation
                    {
                        AnimateRotate = true,
                        AnimateScale = true,
                    },
                    CutoutPercentage = 50,
                    OnClick = new DotNetInstanceClickHandler(ClickItem)
                },
            };
            config.Data.Labels.AddRange(labels);
            var dataset = new PieDataset
            {
                BackgroundColor = new ChartJs.Blazor.ChartJS.Common.IndexableOption<string>(colours.ToArray()),
                
            };
            dataset.Data.AddRange(values);
            config.Data.Datasets.Add(dataset);
            return config;
        }

        protected async Task ShowBreakDown(CategoryDTO category)
        {
            var items = await PlannerItemDataService.LoadCompletedItemsByCategoryID(category.ID);
            var body = new RenderFragment(builder =>
           {
               builder.OpenComponent<EventList>(0);
               builder.AddAttribute(1, nameof(EventListBase.Items), items);
               builder.AddAttribute(2, nameof(EventListBase.Title), $"Completed Items - {category.Description}");
               builder.CloseComponent();
           });
            ModalService.Show(new ModalParams(body, style: ModalStyle.Normal, saveLabel: string.Empty));
        }
    }
}
