using Avalonia;
using Avalonia.Markup.Xaml;
using FluentAvalonia.Styling;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PropertyChanged;
using System;
using System.IO;
using TheXamlGuy.Framework.Avalonia;
using TheXamlGuy.Framework.Core;

namespace Builder
{
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
                    configuration.Add<ProjectConfigurationViewModel, ProjectConfigurationView>("ProjectConfiguration");
                    configuration.Add<StartProjectConfigurationViewModel, StartProjectConfigurationView>("StartProjectConfiguration");
                    configuration.Add<CreateProjectConfigurationViewModel, CreateProjectConfigurationView>("CreateProjectConfiguration");
                    configuration.Add<ExistingProjectConfigurationViewModel, ExistingProjectConfigurationView>("ExistingProjectConfiguration");
                    configuration.Add<ProjectViewModel, ProjectView>("Project");
                    configuration.Add<PageDesignerViewModel, PageDesignerView>("PageDesigner");
                    configuration.Add<PageCollectionViewModel, PageCollectionView>("Pages");
                    configuration.Add<AddPageViewModel, AddPageView>("AddPage");
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
}
