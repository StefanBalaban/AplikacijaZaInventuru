using ApplicationCore.Constants;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

namespace Generator.Generators
{
    public class ArgumentFilterConstantsHelpers
    {
        public string EQUAL = $"{nameof(FilterConstants)}.{nameof(FilterConstants.EQUAL)}";
        public string LT = $"{nameof(FilterConstants)}.{nameof(FilterConstants.LT)}";
        public string GT = $"{nameof(FilterConstants)}.{nameof(FilterConstants.GT)}";
        public string LTE = $"{nameof(FilterConstants)}.{nameof(FilterConstants.LTE)}";
        public string GTE = $"{nameof(FilterConstants)}.{nameof(FilterConstants.GTE)}";
        public List<FilterAndSyntax> FilterAndSyntaxList = new List<FilterAndSyntax>();

        public ArgumentFilterConstantsHelpers()
        {
            FilterAndSyntaxList = new List<FilterAndSyntax>
            {

                new FilterAndSyntax
                {
                    Argument = EQUAL,
                    SyntaxKind = SyntaxKind.EqualsExpression
                },
                new FilterAndSyntax
                {
                    Argument = LTE,
                    SyntaxKind = SyntaxKind.LessThanOrEqualExpression
                },
                new FilterAndSyntax
                {
                    Argument = GTE,
                    SyntaxKind = SyntaxKind.GreaterThanOrEqualExpression
                },
                new FilterAndSyntax
                {
                    Argument = LT,
                    SyntaxKind = SyntaxKind.LessThanExpression
                },
                new FilterAndSyntax
                {
                    Argument = GT,
                    SyntaxKind = SyntaxKind.GreaterThanExpression
                }
            };

        }

        public string GetArgumentSufix(string argument)
        {
            if (argument == EQUAL) return $"";
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