using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class ListEntity<T>
    {
        public IReadOnlyList<T> List = new List<T>();
        public int Count;
    }
}