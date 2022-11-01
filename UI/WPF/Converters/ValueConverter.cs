using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace TheXamlGuy.UI.WPF
{
    public abstract class ValueConverter<TSource, TTarget> : MarkupExtension, IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertTo((TSource)value, targetType, parameter, culture);
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBackTo((TTarget)value, targetType, parameter, culture);
        }

        public TTarget? Convert(TSource value)
        {
            return ConvertTo(value, null, null, null);
        }

        public TSource? ConvertBack(TTarget value)
        {
            return ConvertBackTo(value, null, null, null);
        }

        protected virtual TTarget? ConvertTo(TSource value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            return default;
        }

        protected virtual TSource? ConvertBackTo(TTarget value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            return default;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}