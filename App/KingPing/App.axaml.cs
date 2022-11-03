using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PropertyChanged;
using System;
using System.IO;
using TheXamlGuy.Framework.Avalonia;
using TheXamlGuy.Framework.Core;
using Path = System.IO.Path;

namespace KingPing;

[DoNotNotify]
public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        IHost? host = new HostBuilder()
            .UseContentRoot(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Builder"), true)
            .ConfigureTemplates(configuration =>
            {
                configuration.Add<MainWindowViewModel, MainWindow>();
                configuration.Add<MainViewModel, MainView>("Main");
                configuration.Add<ShellViewModel, ShellView>("Shell");
                configuration.Add<DigitalOutputCollectionViewModel, DigitalOutputCollectionView>("DigitalOutputs");
                configuration.Add<DigitalInputCollectionViewModel, DigitalInputCollectionView>("DigitalInputs");
                configuration.Add<AnalogOutputCollectionViewModel, AnalogOutputCollectionView>("AnalogOutputs");
                configuration.Add<AnalogOutputViewModel, AnalogOutputView>();
                configuration.Add<FavouriteCollectionViewModel, FavouriteCollectionView>("Favourites");
            })
            .ConfigureServices(ConfigureServices)
            .Build();

        await host.RunAsync();
        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddReqiredCore()
            .AddRequiredAvalonia()
            .AddHostedService<AppServices>()
            .RegisterHandlers();
    }
}
