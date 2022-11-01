using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TheXamlGuy.Framework.WPF;
using WeddingBooth.Views;
using System.Reflection;
using System;
using System.IO;
using TheXamlGuy.Framework.Core;
using TheXamlGuy.Framework.Microcontroller;
using TheXamlGuy.Framework.Serial;
using TheXamlGuy.Framework.Camera;
using WeddingBooth.LifeCycles;

namespace TheXamlGuy.App.WeddingDisplay
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs args)
        {
            base.OnStartup(args);

            IHost? host = new HostBuilder()
                .UseContentRoot(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "WeddingBooth"), true)
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.AddWritableJsonFile("Settings.json", false, true, writableConfiguration =>
                    {
                        writableConfiguration.AddDefaultFileStream(Assembly.GetExecutingAssembly().ExtractResource("Settings.json")!)
                            .AddDefaultConfiguration<StartupConfiguration>("Startup")
                            .AddDefaultConfiguration<NavigationConfiguration>("Navigation")
                            .AddDefaultConfiguration<MicrocontrollerConfiguration>("Microcontroller")
                            .AddDefaultConfiguration<RemoteCameraConfiguration>("RemoteCamera");
                    });
                })
                .ConfigureMicrocontrollers((context, builder) =>
                {
                    builder.Add<MicrocontrollerConfiguration, SerialLineReader, string, MicrocontrollerModuleJsonDeserializer>(context.Configuration.GetSection("Microcontroller"))
                        .AddModule<CapactiveSensor>();
                })
                .ConfigureEvents(configuration =>
                {
                    configuration.Add<SerialResponse<string>>().WithHandler(args => args);
                    configuration.Add<Navigated<NavigationView, NavigationViewModel>>().WithHandler(args => args);
                    configuration.Add<Captured>().WithHandler(args => args);
                })
                .ConfigureTemplates(configuration =>
                {
                    configuration.Add<NavigationViewModel, NavigationView>("Navigation");
                    configuration.Add<WelcomeViewModel, WelcomeView>("Welcome");
                    configuration.Add<SeatingChartViewModel, SeatingChartView>("Seatings");
                    configuration.Add<CameraViewModel, CameraView>("Camera");
                    configuration.Add<GalleryViewModel, GalleryView>("Gallery");
                })
                .ConfigureCamera((context, builder) =>
                {
                    builder.Add<RemoteCameraConfiguration>(context.Configuration.GetSection("RemoteCamera"));
                })
                .ConfigureServices(ConfigureServices)
                .Build();

            await host.RunAsync();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddReqiredCore()
                .AddRequiredWpf()
                .AddHostedService<AppServices>()
                .AddSingleton<MainWindow>()
                .AddSingleton<MainWindowViewModel>()
                .AddConfiguration<StartupConfiguration>(context.Configuration.GetSection("Startup"))
                .AddConfiguration<NavigationConfiguration>(context.Configuration.GetSection("Navigation"))
                .AddConfiguration<MicrocontrollerConfiguration>(context.Configuration.GetSection("Microcontroller"))
                .AddConfiguration<RemoteCameraConfiguration>(context.Configuration.GetSection("RemoteCamera"))
                .RegisterHandlers();
        }
    }
}
