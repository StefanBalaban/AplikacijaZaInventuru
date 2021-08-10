using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class ListEntity<T>
    {
        public int Count;
        public IReadOnlyList<T> List = new List<T>();
    }
}