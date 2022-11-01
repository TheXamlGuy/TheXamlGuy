using System;
using System.Windows.Media.Effects;

namespace TheXamlGuy.UI.WPF;

public class ColorComplementEffect : EffectBase
{
    private static readonly PixelShader Shader;

    static ColorComplementEffect()
    {
        Shader = new PixelShader
        {
            UriSource = new Uri("pack://application:,,,/TheXamlGuy.UI.WPF;component/Resources/Effects/ColorComplementEffect.ps")
        };
    }

    public ColorComplementEffect()
    {
        PixelShader = Shader;

        UpdateShaderValue(InputProperty);
    }
}
