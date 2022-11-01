using System;
using System.Windows;
using System.Windows.Data;

namespace TheXamlGuy.UI.WPF
{
    public class PropertyChangedRevoker : DependencyObject, IDisposable
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(object),
                typeof(PropertyChangedRevoker), new PropertyMetadata(null, OnValuePropertyChanged));

        private readonly WeakReference weakPropertySource;

        public PropertyChangedRevoker(DependencyObject propertySource, string path,
            DependencyPropertyChangedEventHandler valueChangedHandler) : this(propertySource, new PropertyPath(path),
            valueChangedHandler)
        {
        }

        public PropertyChangedRevoker(DependencyObject propertySource, DependencyProperty property,
            DependencyPropertyChangedEventHandler valueChangedHandler) : this(propertySource,
            new PropertyPath(property), valueChangedHandler)
        {
        }

        public PropertyChangedRevoker(DependencyObject propertySource, PropertyPath property,
            DependencyPropertyChangedEventHandler valueChangedHandler)
        {
            weakPropertySource = new WeakReference(propertySource);

            Binding binding = new Binding
            {
                Path = property,
                Mode = BindingMode.OneWay,
                Source = propertySource
            };

            BindingOperations.SetBinding(this, ValueProperty, binding);
            ValueChanged = valueChangedHandler;
            Property = property;
        }

        public DependencyObject PropertySource
        {
            get
            {
                try
                {
                    return weakPropertySource.IsAlive ? weakPropertySource.Target as DependencyObject : null;
                }
                catch
                {
                    return null;
                }
            }
        }

        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public PropertyPath Property { get; }

        public void Dispose()
        {
            BindingOperations.ClearBinding(this, ValueProperty);
        }

        private event DependencyPropertyChangedEventHandler ValueChanged;

        private static void OnValuePropertyChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs args)
        {
            PropertyChangedRevoker sender = dependencyObject as PropertyChangedRevoker;
            sender?.OnValuePropertyChanged(args);
        }

        private void OnValuePropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            ValueChanged?.Invoke(this, args);
        }
    }
}