using System.Windows.Data;

namespace TheXamlGuy.UI.WPF
{
    public static class MarkupExtensions
    {
        public static Binding ToBinding(this object value)
        {
            return value is Binding binding ? binding : new Binding() { Mode = BindingMode.OneWay, Source = value };
        }
    }
}