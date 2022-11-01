using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows;

namespace TheXamlGuy.UI.WPF;

public abstract class EffectBase : ShaderEffect
{
    public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof(EffectBase), 0);

    public Brush Input
    {
        get => (Brush)GetValue(InputProperty);
        set => SetValue(InputProperty, value);
    }
}
