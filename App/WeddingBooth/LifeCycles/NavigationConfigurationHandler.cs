using TheXamlGuy.Framework.Core;
using TheXamlGuy.Framework.WPF;
using WeddingBooth.Views;

namespace WeddingBooth.LifeCycles
{

    public class NavigationNavigatedHandler : IMediatorHandler<Navigated<NavigationView, NavigationViewModel>>
    {
        private readonly NavigationConfiguration configuration;

        public NavigationNavigatedHandler(NavigationConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Handle(Navigated<NavigationView, NavigationViewModel> request)
        {
            foreach (string navigation in configuration)
            {
                switch (navigation)
                {
                    case "Welcome":
                        request.DataContext?.Add<WelcomeViewModel>();
                        break;
                    case "SeatingChart":
                        request.DataContext?.Add<SeatingChartViewModel>();
                        break;
                    case "Camera":
                        request.DataContext?.Add<CameraViewModel>();
                        break;
                    case "Gallery":
                        request.DataContext?.Add<GalleryViewModel>();
                        break;
                }
            }
        }
    }
}
