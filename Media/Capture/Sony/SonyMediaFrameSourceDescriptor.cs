using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace TheXamlGuy.Media.Capture;

public class SonyMediaFrameSourceDescriptor : IRemoteMediaFrameSourceDescriptor
{
    public SonyMediaFrameSourceDescriptor(string id, string displayName, Func<IRemoteMediaFrameReader> factory)
    {
        Id = id;
        DisplayName = displayName;
        FrameReaderFactory = factory;
    }

    public string DisplayName { get; }

    public string Id { get; }

    public Func<IRemoteMediaFrameReader> FrameReaderFactory { get; }

    public static async Task<IRemoteMediaFrameSourceDescriptor?> CreateAsync(Uri descriptionLocation)
    {
        using HttpClient httpClient = new();
        HttpResponseMessage responseMessage = await httpClient.GetAsync(descriptionLocation);
        string content = await responseMessage.Content.ReadAsStringAsync();

        XmlSerializer serializer = new(typeof(Descriptor));
        using TextReader reader = new StringReader(content);

        if ((Descriptor?)serializer.Deserialize(reader) is Descriptor result && result.Device is Device device && device.DeviceInfo is DeviceInfo deviceInfo
                && deviceInfo.ServiceList is ServiceList serviceList && serviceList.Service is List<Service> services
                && services.FirstOrDefault(x => x.ServiceType is string serviceType
                && serviceType.Equals("camera", StringComparison.InvariantCultureIgnoreCase)) is Service service)
        {
            return new SonyMediaFrameSourceDescriptor(device.UDN ?? "", device.FriendlyName ?? "", () => new SonyMediaFrameReader($"{service.Action}\\{service.ServiceType}"));
        }

        return default;
    }

    private record Initialize() : Request("startRecMode", "1.0", Array.Empty<string>());

    private record Capture() : Request("actTakePicture", "1.0", Array.Empty<string>())
    {
        [JsonPropertyName("result")]
        public List<List<string>>? Captures { get; set; }
    }

    private record Request
    {
        private static int requestId;

        public Request(string method, string version = "1.0", params string[] parameters)
        {
            Method = method;
            Version = version;
            Id = GenerateId();
            Paramaters = parameters;
        }

        private static int GenerateId()
        {
            int id = Interlocked.Increment(ref requestId);
            if (requestId > 1000000000)
            {
                requestId = 0;
            }
            return id;
        }

        [JsonPropertyName("method")]
        public string? Method { get; init; }

        [JsonPropertyName("version")]
        public string? Version { get; init; }

        [JsonPropertyName("id")]
        public int? Id { get; init; }

        [JsonPropertyName("params")]
        public string[]? Paramaters { get; init; }

        public static Request CreateRequest(string method, string version = "1.0", params string[] parameters)
        {
            return new(method, version, parameters);
        }
    }
    [XmlRoot(ElementName = "root", Namespace = "urn:schemas-upnp-org:device-1-0")]
    public class Descriptor
    {
        [XmlElement(ElementName = "device", Namespace = "urn:schemas-upnp-org:device-1-0")]
        public Device? Device { get; set; }
    }

    [XmlRoot(ElementName = "device", Namespace = "urn:schemas-upnp-org:device-1-0")]
    public class Device
    {
        [XmlElement(ElementName = "X_ScalarWebAPI_DeviceInfo", Namespace = "urn:schemas-sony-com:av")]
        public DeviceInfo? DeviceInfo { get; set; }

        [XmlElement(ElementName = "friendlyName", Namespace = "urn:schemas-upnp-org:device-1-0")]
        public string? FriendlyName { get; set; }

        [XmlElement(ElementName = "manufacturer", Namespace = "urn:schemas-upnp-org:device-1-0")]
        public string? Manufacturer { get; set; }

        [XmlElement(ElementName = "modelDescription", Namespace = "urn:schemas-upnp-org:device-1-0")]
        public string? ModelDescription { get; set; }

        [XmlElement(ElementName = "modelName", Namespace = "urn:schemas-upnp-org:device-1-0")]
        public string? ModelName { get; set; }

        [XmlElement(ElementName = "UDN", Namespace = "urn:schemas-upnp-org:device-1-0")]
        public string? UDN { get; set; }
    }

    [XmlRoot(ElementName = "X_ScalarWebAPI_DeviceInfo", Namespace = "urn:schemas-sony-com:av")]
    public class DeviceInfo
    {
        [XmlElement(ElementName = "X_ScalarWebAPI_ServiceList", Namespace = "urn:schemas-sony-com:av")]
        public ServiceList? ServiceList { get; set; }
    }

    [XmlRoot(ElementName = "X_ScalarWebAPI_Service", Namespace = "urn:schemas-sony-com:av")]
    public class Service
    {
        [XmlElement(ElementName = "X_ScalarWebAPI_ActionList_URL", Namespace = "urn:schemas-sony-com:av")]
        public string? Action { get; set; }

        [XmlElement(ElementName = "X_ScalarWebAPI_ServiceType", Namespace = "urn:schemas-sony-com:av")]
        public string? ServiceType { get; set; }
    }

    [XmlRoot(ElementName = "X_ScalarWebAPI_ServiceList", Namespace = "urn:schemas-sony-com:av")]
    public class ServiceList
    {
        [XmlElement(ElementName = "X_ScalarWebAPI_Service", Namespace = "urn:schemas-sony-com:av")]
        public List<Service>? Service { get; set; }
    }
}
