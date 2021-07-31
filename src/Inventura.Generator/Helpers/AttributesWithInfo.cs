using System.Collections.Generic;

namespace Generator.Generators
{
    public class AttributesWithInfo
    {
        public string PropertyIdentifier { get; set; }
        public List<string> Arguments { get; set; } = new List<string>();
        public string Type { get; set; }
    }
}