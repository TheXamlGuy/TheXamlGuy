using System.Windows.Data;
using System.Windows.Markup;

namespace TheXamlGuy.UI.WPF;

public class EventParameterExtension : MarkupExtension, IEventParameter
{
    private readonly IValueConverter? converter;
    private readonly object? converterParameter;
    private readonly string? key;
    private readonly string? path;

    public EventParameterExtension()
    {

    }

    public EventParameterExtension(string key, string path)
    {
        this.key = key;
        this.path = path;
    }

    public EventParameterExtension(string path)
    {
        this.path = path;
    }


    public EventParameterExtension(string path, IValueConverter converter)
    {
        this.path = path;
        this.converter = converter;
    }

    public EventParameterExtension(string path, IValueConverter converter, object converterParameter)
    {
        this.path = path;
        this.converter = converter;
        this.converterParameter = converterParameter;
    }

    public List<object> GetParameters(EventArgs args)
    {
        List<object>? parameters = new();

        dynamic? arguments = args.GetEventArguments(path, converter, converterParameter);
        if (arguments is not null)
        {
            if (arguments is ICollection<object> collection)
            {
                foreach (object? argument in collection)
                {
                    parameters.Add(key is not null ? new KeyValuePair<string, object>(key, (dynamic)argument) : argument);
                }
            }
            else
            {
                parameters.Add(key is not null ? new KeyValuePair<string, object>(key, arguments) : arguments);
            }
        }

        return parameters;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}