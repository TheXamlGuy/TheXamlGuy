using System.Collections.Generic;

namespace Builder.LifeCycles
{
    public interface IProjectContext
    {
        ICollection<Page> Pages { get; }
    }
}