using System.Collections.Generic;

namespace WeddingBooth.LifeCycles
{
    public class Table
    {
        public string? Name { get; set; }

        public List<string> Guests { get; set; } = new List<string>();
    }
}
