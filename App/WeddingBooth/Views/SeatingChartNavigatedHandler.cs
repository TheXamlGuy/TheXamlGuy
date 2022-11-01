using TheXamlGuy.Framework.Core;
using TheXamlGuy.Framework.WPF;
using WeddingBooth.Views;

namespace WeddingBooth.LifeCycles
{
    public class SeatingChartNavigatedHandler : IMediatorHandler<Navigated<SeatingChartView, SeatingChartViewModel>>
    {
        private readonly SeatingChartConfiguration configuration;

        public SeatingChartNavigatedHandler(SeatingChartConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Handle(Navigated<SeatingChartView, SeatingChartViewModel> request)
        {
            foreach (Table table in configuration)
            {
                if(request.DataContext?.Add(table.Name) is TableViewModel tableViewModel)
                {
                    foreach (string guest in table.Guests)
                    {
                        tableViewModel.Add(guest);
                    }
                }
            }
        }
    }
}
