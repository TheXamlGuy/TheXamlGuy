using System.ComponentModel;
using System.Globalization;

namespace TheXamlGuy.UI;

public static class TypeConverterHelper
{
    public static object? DoConversionFrom(TypeConverter converter, object value)
    {
        object? returnValue = value;

        try
        {
            if (converter != null && value != null && converter.CanConvertFrom(value.GetType()))
            {
                returnValue = converter.ConvertFrom(null, CultureInfo.InvariantCulture, value);
            }
        }
        catch (Exception exception) when (ShouldEatException(exception))
        {
        }

        return returnValue;
    }

    private static bool ShouldEatException(Exception exception)
    {
        bool shouldEat = false;
        if (exception.InnerException != null)
        {
            shouldEat |= ShouldEatException(exception.InnerException);
        }

        shouldEat |= exception is FormatException;
        return shouldEat;
    }

    public static TypeConverter GetTypeConverter(Type type)
    {
        return TypeDescriptor.GetConverter(type);
    }
}