using System.Collections.Generic;

namespace Builder.LifeCycles
{
    public class ProjectContext : IProjectContext
    {
        public ICollection<Page> Pages => new List<Page>();
    }

    public interface IProjectScope
    {

    }
}
