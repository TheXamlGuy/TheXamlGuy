using Avalonia.Controls.Primitives;
using TheXamlGuy.Framework.Core;

namespace TheXamlGuy.Framework.Avalonia;

public abstract class RouteHandler<TTarget> : IMediatorHandler<Route<TTarget>> where TTarget : TemplatedControl
{
    public abstract void Handle(Route<TTarget> request);
}
