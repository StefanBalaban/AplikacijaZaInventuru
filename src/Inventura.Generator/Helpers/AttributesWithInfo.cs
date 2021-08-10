using System.Collections.Generic;

namespace Generator.Generators
{
    public class AttributesWithInfo
    {
        public string PropertyIdentifier { get; set; }
        public List<string> Arguments { get; set; } = new();
        public string Type { get; set; }
    }
}