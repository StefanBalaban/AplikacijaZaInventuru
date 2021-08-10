using System.Collections.Generic;
using ApplicationCore.Constants;
using Microsoft.CodeAnalysis.CSharp;

namespace Generator.Generators
{
    public class ArgumentFilterConstantsHelpers
    {
        public string EQUAL = $"{nameof(FilterConstants)}.{nameof(FilterConstants.EQUAL)}";
        public List<FilterAndSyntax> FilterAndSyntaxList = new();
        public string GT = $"{nameof(FilterConstants)}.{nameof(FilterConstants.GT)}";
        public string GTE = $"{nameof(FilterConstants)}.{nameof(FilterConstants.GTE)}";
        public string LT = $"{nameof(FilterConstants)}.{nameof(FilterConstants.LT)}";
        public string LTE = $"{nameof(FilterConstants)}.{nameof(FilterConstants.LTE)}";

        public ArgumentFilterConstantsHelpers()
        {
            FilterAndSyntaxList = new List<FilterAndSyntax>
            {
                new()
                {
                    Argument = EQUAL,
                    SyntaxKind = SyntaxKind.EqualsExpression
                },
                new()
                {
                    Argument = LTE,
                    SyntaxKind = SyntaxKind.LessThanOrEqualExpression
                },
                new()
                {
                    Argument = GTE,
                    SyntaxKind = SyntaxKind.GreaterThanOrEqualExpression
                },
                new()
                {
                    Argument = LT,
                    SyntaxKind = SyntaxKind.LessThanExpression
                },
                new()
                {
                    Argument = GT,
                    SyntaxKind = SyntaxKind.GreaterThanExpression
                }
            };
        }

        public string GetArgumentSufix(string argument)
        {
            if (argument == EQUAL) return "";
            if (argument == LT) return $"{nameof(LT)}";
            if (argument == GT) return $"{nameof(GT)}";
            if (argument == LTE) return $"{nameof(LTE)}";
            if (argument == GTE) return $"{nameof(GTE)}";
            return default;
        }
    }

    public class FilterAndSyntax
    {
        public string Argument { get; set; }
        public SyntaxKind SyntaxKind { get; set; }
    }
}