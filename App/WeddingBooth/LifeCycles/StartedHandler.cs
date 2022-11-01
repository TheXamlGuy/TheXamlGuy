using System.Linq;
using System.Windows;
using TheXamlGuy.Framework.Core;
using WeddingBooth.Views;
using WpfScreenHelper;

namespace WeddingBooth.LifeCycles
{
    public class StartedHandler : IMediatorHandler<Started>
    {
        private readonly StartupConfiguration configuration;
        private readonly MainWindow window;
        private readonly MainWindowViewModel viewModel;

        public StartedHandler(StartupConfiguration configuration,
            MainWindow window, 
            MainWindowViewModel viewModel)
        {
            this.configuration = configuration;
            this.window = window;
            this.viewModel = viewModel;
        }

        public void Handle(Started request)
        {
            window.DataContext = viewModel;
            window.Show();

            if (configuration.Display is string display)
            {
                Screen? screen = Screen.AllScreens.FirstOrDefault(x => x.DeviceName.Contains(display, System.StringComparison.InvariantCultureIgnoreCase)) ?? Screen.AllScreens.FirstOrDefault();     
                if (screen is not null)
                {
                    window.Left = screen.Bounds.Left;
                    window.Top = screen.Bounds.Top;
                    window.Width = screen.Bounds.Width;
                    window.Height = screen.Bounds.Height;
                }

                if (configuration.FullScreen)
                {
                    window.WindowState = WindowState.Maximized;
                }
            }
        }
    }
}
