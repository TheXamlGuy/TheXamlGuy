using System;
using System.Windows;
using System.Windows.Media.Effects;

namespace TheXamlGuy.UI.WPF;

public class GrayScaleEffect : EffectBase
{
    public static readonly DependencyProperty ScaleProperty = 
        DependencyProperty.Register("Scale", 
            typeof(double), typeof(GrayScaleEffect), new PropertyMetadata(ValueBoxes.Double1Box, PixelShaderConstantCallback(0)));

    private static readonly PixelShader Shader;

    static GrayScaleEffect()
    {
        Shader = new PixelShader
        {
            UriSource = new Uri("pack://application:,,,/TheXamlGuy.UI.WPF;component/Resources/Effects/GrayScaleEffect.ps")
        };
    }

    public GrayScaleEffect()
    {
        PixelShader = Shader;

        UpdateShaderValue(InputProperty);
        UpdateShaderValue(ScaleProperty);
    }

    public double Scale
    {
        get => (double) GetValue(ScaleProperty);
        set => SetValue(ScaleProperty, value);
    }
}
