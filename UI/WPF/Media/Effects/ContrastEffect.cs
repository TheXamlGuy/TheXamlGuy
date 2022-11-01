using System;
using System.Windows;
using System.Windows.Media.Effects;

namespace TheXamlGuy.UI.WPF;

public class ContrastEffect : EffectBase
{
    public static readonly DependencyProperty ContrastProperty =
        DependencyProperty.Register("Contrast",
            typeof(double), typeof(ContrastEffect), new PropertyMetadata(ValueBoxes.Double1Box, PixelShaderConstantCallback(0)));

    private static readonly PixelShader Shader;

    static ContrastEffect()
    {
        Shader = new PixelShader
        {
            UriSource = new Uri("pack://application:,,,/TheXamlGuy.UI.WPF;component/Resources/Effects/ContrastEffect.ps")
        };
    }

    public ContrastEffect()
    {
        PixelShader = Shader;

        UpdateShaderValue(InputProperty);
        UpdateShaderValue(ContrastProperty);
    }

    public double Contrast
    {
        get => (double) GetValue(ContrastProperty);
        set => SetValue(ContrastProperty, value);
    }
}
