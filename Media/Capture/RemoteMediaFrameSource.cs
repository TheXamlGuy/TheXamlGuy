using Rssdp;
using static TheXamlGuy.Media.Capture.SonyMediaFrameSourceDescriptor;

namespace TheXamlGuy.Media.Capture;

public class RemoteMediaFrameSource : IRemoteMediaFrameSource, IRemoteMediaFrameReaderFactory
{
    private static readonly Dictionary<string, Func<Uri, Task<IRemoteMediaFrameSourceDescriptor>>> supportedDeviceSchemas = new()
    {
        { "urn:schemas-sony-com:service:ScalarWebAPI:1", async args => (await CreateAsync(args))! }
    };

    internal RemoteMediaFrameSource(string id, string displayName, Func<IRemoteMediaFrameReader> factory)
    {
        Info = new MediaFrameSourceInfo(id);
        DisplayName = displayName;
        Factory = factory;
    }

    public string DisplayName { get; }

    public MediaFrameSourceInfo Info { get; }

    public Func<IRemoteMediaFrameReader> Factory { get; }

    public static async Task<IReadOnlyList<IRemoteMediaFrameSource>> FindAllAsync()
    {
        List<IRemoteMediaFrameSource> result = new();

        using (SsdpDeviceLocator deviceLocator = new())
        {
            IEnumerable<DiscoveredSsdpDevice> foundDevices = await deviceLocator.SearchAsync();
            foreach (DiscoveredSsdpDevice foundDevice in foundDevices)
            {
                if (supportedDeviceSchemas.TryGetValue(foundDevice.NotificationType, out Func<Uri, Task<IRemoteMediaFrameSourceDescriptor>>? factory))
                {
                    if (await factory.Invoke(foundDevice.DescriptionLocation) is IRemoteMediaFrameSourceDescriptor descriptor)
                    {
                        result.Add(new RemoteMediaFrameSource(descriptor.Id, descriptor.DisplayName, descriptor.FrameReaderFactory));
                    }
                }
            }
        }

        return await Task.FromResult(result);
    }
}
