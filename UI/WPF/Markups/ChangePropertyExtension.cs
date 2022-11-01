using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace TheXamlGuy.UI.WPF;

public class ChangePropertyExtension : TriggerExtension
{
    private static readonly DependencyProperty PropertyTargetProperty =
        DependencyProperty.RegisterAttached("PropertyTarget",
            typeof(object), typeof(ChangePropertyExtension));

    private static readonly DependencyProperty PropertyValueProperty =
        DependencyProperty.RegisterAttached("PropertyValue",
            typeof(object), typeof(ChangePropertyExtension));

    private readonly string propertyName;

    private readonly BindingBase targetBinding;

    private readonly BindingBase valueBinding;

    public ChangePropertyExtension(object target, string propertyName, object value)
    {
        this.targetBinding = target is BindingBase targetBinding ? targetBinding : target.ToBinding();
        this.valueBinding = value is BindingBase valueBinding ? valueBinding : value.ToBinding();

        this.propertyName = propertyName;
    }

    protected override void OnInvoked(object sender, EventArgs args)
    {
        BindingOperations.SetBinding(TargetObject, PropertyTargetProperty, targetBinding);
        if (TargetObject?.GetValue(PropertyTargetProperty) is { } target)
        {
            Type? targetType = target.GetType();
            if (targetType.GetProperty(propertyName) is PropertyInfo propertyInfo)
            {
                BindingOperations.SetBinding(TargetObject, PropertyValueProperty, valueBinding);
                object? value = TargetObject?.GetValue(PropertyValueProperty);

                TypeConverter? converter = TypeConverterHelper.GetTypeConverter(propertyInfo.PropertyType);
                object? newValue = value;

                if (value is not null)
                {
                    if (converter?.CanConvertFrom(value.GetType()) == true)
                    {
                        newValue = converter.ConvertFrom(null, CultureInfo.InvariantCulture, value);
                    }
                    else
                    {
                        if (converter?.CanConvertTo(propertyInfo.PropertyType) == true)
                        {
                            newValue = converter.ConvertTo(null, CultureInfo.InvariantCulture, value, propertyInfo.PropertyType);
                        }
                    }
                }

                if (newValue is IEventParameter eventParameter)
                {
                    newValue = eventParameter.GetParameters(args)[0];
                }

                propertyInfo?.SetValue(target, newValue, Array.Empty<object>());
            }
        }
    }
}