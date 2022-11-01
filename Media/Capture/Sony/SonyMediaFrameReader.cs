using System.Drawing;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using TheXamlGuy.UI;

namespace TheXamlGuy.Media.Capture;

public class SonyMediaFrameReader : IRemoteMediaFrameReader
{
    private readonly HttpClient client;
    private readonly string endpoint;

    public SonyMediaFrameReader(string endpoint)
    {
        client = new HttpClient();
        this.endpoint = endpoint;
    }

    public event TypedEventHandler<IMediaFrameReader, MediaFrameArrivedEventArgs>? FrameArrived;

    public void Dispose()
    {

    }

    public async Task StartAsync()
    {
        await PostRequestAsync<Initialize>();
    }

    public async Task StopAsync()
    {
        await Task.CompletedTask;
    }

    public async Task<MediaFrame?> TryAcquireLatestFrameAsync()
    {
        await PostRequestAsync<Initialize>();

        if (await PostRequestAsync<Capture>() is Capture result && result.Captures is not null )
        {
            if (result.Captures.FirstOrDefault() is List<string> captures)
            {
                if (captures.FirstOrDefault() is string url)
                {
                    HttpResponseMessage content = await client.GetAsync(url);

                    using Stream stream = content.Content.ReadAsStream();
                    using Image bitmap = Image.FromStream(stream);
                    return new MediaFrame((Bitmap)bitmap.Clone(), bitmap.Width, bitmap.Height);
                }
            }
        }

        return default;
    }

    private async Task<TReponse?> PostRequestAsync<TRequest, TReponse>() where TRequest : Request, new()
    {
        try
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(endpoint, new TRequest());
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TReponse>();
            }

        }
        catch
        {

        }

        return default;
    }

    private async Task<TRequest?> PostRequestAsync<TRequest>() where TRequest : Request, new()
    {
        return await PostRequestAsync<TRequest, TRequest>();
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
}
