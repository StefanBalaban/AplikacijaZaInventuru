using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator.Generators
{
    public class PropertiesHelper
    {
        public List<KeyValuePair<string, string>> PropertiesWithAttributes = new();
        public List<PropertiesWithInfo> PropertiesWithInfos { get; set; } = new();
        public List<AttributesWithInfo> AttributesWithInfo { get; set; } = new();

        public void ExtractPropertiesAndAttrbitues(IEnumerable<MemberDeclarationSyntax> members)
        {
            foreach (var memberDeclarationSyntax in members)
            {
                var attributeNames = new List<string>();
                var property = memberDeclarationSyntax as PropertyDeclarationSyntax;
                if (property != null)
                {
                    var attributes = property.AttributeLists.ToList();
                    foreach (var attributeListSyntax in attributes)
                    {
                        var attribute = attributeListSyntax.Attributes.FirstOrDefault();
                        if (attribute != null)
                        {
                            var attributeName = attribute.Name.NormalizeWhitespace().ToFullString();
                            attributeNames.Add(attributeName);
                            if (attribute.Name.NormalizeWhitespace().ToFullString()
                                .Equals("Get"))
                            {
                                var arguments = attribute.ArgumentList?.Arguments;
                                if (arguments.HasValue)
                                    AttributesWithInfo.Add(new AttributesWithInfo
                                    {
                                        PropertyIdentifier = property.Identifier.Text,
                                        Arguments = AddArguments(arguments.Value.ToList()),
                                        Type = property.Type.NormalizeWhitespace().ToFullString()
                                    });
                            }

                            if (attributeName.Equals("Post") || attributeName.Equals("Get") ||
                                attributeName.Equals("Put") || attributeName.Equals("Dto"))
                                PropertiesWithInfos.Add(new PropertiesWithInfo
                                {
                                    Identifier = property.Identifier.Text,
                                    Type = property.Type.NormalizeWhitespace().ToFullString(),
                                    Method = attributeName
                                });
                        }
                    }

                    if (attributeNames != null)
                        attributeNames.ForEach(x =>
                            PropertiesWithAttributes.Add(
                                new KeyValuePair<string, string>(property.Identifier.Text, x)));
                }
            }
        }

        private List<string> AddArguments(List<AttributeArgumentSyntax> arguments)
        {
            var argumentList = new List<string>();
            foreach (var argument in arguments)
                argumentList.Add(argument.NormalizeWhitespace().ToFullString());

            return argumentList;
        }
    }
}