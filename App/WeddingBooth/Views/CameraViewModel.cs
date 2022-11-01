using System;
using TheXamlGuy.Framework.Camera;
using TheXamlGuy.Framework.Core;

namespace WeddingBooth.Views;

public class CameraViewModel : ObservableViewModel
{
    public CameraViewModel(IPropertyBuilder propertyBuilder, 
        IEventAggregator eventAggregator,
        IServiceFactory serviceFactory, 
        IDisposer disposer) : base(propertyBuilder, eventAggregator, serviceFactory, disposer)
    {
    }

    public bool CanCapture { get; set; } = true;

    public void Capture()
    {
        EventAggregator.Publish<Capture>();
    }
}
