using Avalonia.Controls.Primitives;

namespace TheXamlGuy.Framework.Avalonia;

public record Route<TTarget>(TTarget Target, object? Data, object? Template) where TTarget : TemplatedControl;
