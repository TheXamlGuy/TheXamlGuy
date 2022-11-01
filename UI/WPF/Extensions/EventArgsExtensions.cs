using System;
using System.Globalization;
using System.Windows.Data;

namespace TheXamlGuy.UI.WPF;

public static class EventArgsExtensions
{
    public static dynamic? GetEventArguments(this EventArgs args, string? path, IValueConverter? converter, object? converterParameter)
    {
        if (path is { Length: > 0 })
        {
            if (GetEventArgsPropertyPathValue(args, path) is object value)
            {
                if (converter is not null)
                {
                    return converter.Convert(value, typeof(object), converterParameter, CultureInfo.CurrentCulture);
                }

                return value;
            }
        }

        return args;
    }

    private static object GetEventArgsPropertyPathValue(object args, string path)
    {
        object? value = args;
        if (path is { })
        {
            value = PropertyPathHelper.GetValue(args, path);
        }

        return value;
    }
}