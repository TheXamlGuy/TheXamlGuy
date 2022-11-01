using System.Windows.Data;

namespace TheXamlGuy.UI.WPF;

public class ParameterExtension : Binding, IParameter
{
    public ParameterExtension(string key)
    {
        Key = key;
    }

    public ParameterExtension(string key, string path) : base(path)
    {
        Key = key;
    }

    public string? Key { get; }
}