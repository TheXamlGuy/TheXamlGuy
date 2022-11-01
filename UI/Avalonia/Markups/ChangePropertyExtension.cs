using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace TheXamlGuy.UI.Avalonia;

public class ChangePropertyExtension : TriggerExtension
{
    private static readonly AvaloniaProperty PropertyTargetProperty =
        AvaloniaProperty.RegisterAttached<ChangePropertyExtension, Control, object>("PropertyTarget");

    private static readonly AvaloniaProperty PropertyValueProperty =
        AvaloniaProperty.RegisterAttached<ChangePropertyExtension, Control, object>("PropertyValue");

    private readonly string name;

    private readonly BindingBase targetBinding;
    private readonly BindingBase valueBinding;

    public ChangePropertyExtension(object target, string name, object value)
    {
        this.targetBinding = target is BindingBase targetBinding ? targetBinding : target.ToBinding();
        this.valueBinding = value is BindingBase valueBinding ? valueBinding : value.ToBinding();

        this.name = name;
    }

    protected override void OnInvoked(object sender, EventArgs args)
    {
        TargetObject?.Bind(PropertyTargetProperty, targetBinding);
        if (TargetObject?.GetValue(PropertyTargetProperty) is { } target)
        {
            Type? targetType = target.GetType();

            if (targetType.GetProperty(name) is PropertyInfo propertyInfo)
            {
                TargetObject?.Bind(PropertyValueProperty, valueBinding);
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

                if (target is TemplatedControl control )
                {
                    if (AvaloniaPropertyRegistry.Instance.FindRegistered(targetType, name) is AvaloniaProperty property)
                    {
                        control.SetValue(property, newValue);
                    }

                    if (propertyInfo.PropertyType == typeof(Classes))
                    {
                        control.Classes.Clear();
                        control.Classes.Add($"{newValue}");
                    }
                }
                else
                {
                    propertyInfo?.SetValue(target, newValue, Array.Empty<object>());
                }
            }
        }
    }
}
