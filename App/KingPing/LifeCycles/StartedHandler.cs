using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using TheXamlGuy.Framework.Core;

namespace KingPing;

public class StartedHandler : IMediatorHandler<Started>
{
    private readonly MainWindow window;
    private readonly MainWindowViewModel viewModel;

    public StartedHandler(MainWindow window,
        MainWindowViewModel viewModel)
    {
        this.window = window;
        this.viewModel = viewModel;
    }

    public void Handle(Started request)
    {
        window.DataContext = viewModel;
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = window;
        }
    }
}
