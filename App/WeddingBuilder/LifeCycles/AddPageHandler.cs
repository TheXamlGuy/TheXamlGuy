using TheXamlGuy.Framework.Core;

namespace Builder.LifeCycles;

public class AddPageHandler : IMediatorHandler<AddPage>
{
    private readonly IProjectContext context;

    public AddPageHandler(IProjectContext context)
    {
        this.context = context;
    }

    public void Handle(AddPage request)
    {
        context.Pages.Add(new Page(request.Name));
    }
}
