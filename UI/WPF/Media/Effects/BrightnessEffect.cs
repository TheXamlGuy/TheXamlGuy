using System.Windows.Media.Effects;
using System.Windows;
using System;

namespace TheXamlGuy.UI.WPF;

public class BrightnessEffect : EffectBase
{
    public static readonly DependencyProperty BrightnessProperty = 
        DependencyProperty.Register(nameof(Brightness), typeof(double),
            typeof(BrightnessEffect), new PropertyMetadata(ValueBoxes.Double1Box, PixelShaderConstantCallback(0)));

    private static readonly PixelShader Shader;

    static BrightnessEffect()
    {
        Shader = new PixelShader
        {
            UriSource = new Uri("pack://application:,,,/TheXamlGuy.UI.WPF;component/Resources/Effects/BrightnessEffect.ps")
        };
    }

    public BrightnessEffect()
    {
        PixelShader = Shader;

        UpdateShaderValue(InputProperty);
        UpdateShaderValue(BrightnessProperty);
    }
    public double Brightness
    {
        get => (double)GetValue(BrightnessProperty);
        set => SetValue(BrightnessProperty, value);
    }
}