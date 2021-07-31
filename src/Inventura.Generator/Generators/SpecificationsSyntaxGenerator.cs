using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Generator.Generators
{
    internal class SpecificationsSyntaxGenerator
    {
        private readonly ArgumentFilterConstantsHelpers _argumentFilterConstantsHelpers = new ArgumentFilterConstantsHelpers();
        private List<AttributesWithInfo> _attributes = new List<AttributesWithInfo>();
        private string _modelClassName;

        public SyntaxNode GenerateSpecificationsNode(string modelClassName,
            List<AttributesWithInfo> attributes)
        {
            _modelClassName = modelClassName;
            _attributes = attributes;

            return GenerateSpecificationsNode();
        }

        private SyntaxNode GenerateSpecificationsNode()
        {
            return
                CompilationUnit()
                    .WithUsings(
                        List(
                            new[]
                            {
                                UsingDirective(
                                    QualifiedName(
                                        IdentifierName("Ardalis"),
                                        IdentifierName("Specification"))),
                                UsingDirective(
                                    IdentifierName("Entities"))
                            }))
                    .WithMembers(
                        SingletonList<MemberDeclarationSyntax>(
                            NamespaceDeclaration(
                                    QualifiedName(
                                        QualifiedName(
                                            IdentifierName("ApplicationCore"),
                                            IdentifierName("Specifications")),
                                        IdentifierName(_modelClassName)))
                                .WithMembers(
                                    List(
                                        new MemberDeclarationSyntax[]
                                        {
                                            ClassDeclaration($"{_modelClassName}FilterPaginatedSpecification")
                                                .WithModifiers(
                                                    TokenList(
                                                        Token(SyntaxKind.PublicKeyword)))
                                                .WithBaseList(
                                                    BaseList(
                                                        SingletonSeparatedList<BaseTypeSyntax>(
                                                            SimpleBaseType(
                                                                GenericName(
                                                                        Identifier("Specification"))
                                                                    .WithTypeArgumentList(
                                                                        TypeArgumentList(
                                                                            SingletonSeparatedList<TypeSyntax>(
                                                                                IdentifierName(_modelClassName))))))))
                                                .WithMembers(
                                                    SingletonList(GeneratePaginatedSpecificationConstructor())
                                                ),
                                            ClassDeclaration($"{_modelClassName}FilterSpecification")
                                                .WithModifiers(
                                                    TokenList(
                                                        Token(SyntaxKind.PublicKeyword)))
                                                .WithBaseList(
                                                    BaseList(
                                                        SingletonSeparatedList<BaseTypeSyntax>(
                                                            SimpleBaseType(
                                                                GenericName(
                                                                        Identifier("Specification"))
                                                                    .WithTypeArgumentList(
                                                                        TypeArgumentList(
                                                                            SingletonSeparatedList<TypeSyntax>(
                                                                                    IdentifierName(_modelClassName))))))))
                                                .WithMembers(
                                                    SingletonList(
                                                        GenerateSpecificationConstructor()))
                                        }))))
                    .NormalizeWhitespace();
        }

        private MemberDeclarationSyntax GenerateSpecificationConstructor()
        {
            return
                ConstructorDeclaration(
                        Identifier($"{_modelClassName}FilterSpecification"))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SeparatedList<ParameterSyntax>(GetSpecificationsConstructorParameters())))
                    .WithBody(
                        Block(GetSpecificationConstructorBody()));
        }


        private MemberDeclarationSyntax GeneratePaginatedSpecificationConstructor()
        {
            return
                ConstructorDeclaration(
                        Identifier($"{_modelClassName}FilterPaginatedSpecification"))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SeparatedList<ParameterSyntax>(GetPaginatedSpecificationsConstructorParameters())))
                    .WithBody(
                        Block(GetPagedSpecificationConstructorBody()));
        }

        private IEnumerable<StatementSyntax> GetPagedSpecificationConstructorBody()
        {
            var statementSyntaxList = GetSpecificationConstructorBody();
            statementSyntaxList.Add(ExpressionStatement(
                InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("Query"),
                            IdentifierName("Skip")))
                    .WithArgumentList(
                        ArgumentList(
                            SingletonSeparatedList(
                                Argument(
                                    IdentifierName("skip")))))));
            statementSyntaxList.Add(
                ExpressionStatement(
                    InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("Query"),
                                IdentifierName("Take")))
                        .WithArgumentList(
                            ArgumentList(
                                SingletonSeparatedList(
                                    Argument(
                                        IdentifierName("take")))))));

            _attributes.ForEach(x =>
                x.Arguments.Where(y => y.Contains("INCLUDE")).ToList().ForEach(z =>
                    statementSyntaxList.Add(ExpressionStatement(
                        InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("Query"),
                                    IdentifierName("Include")))
                            .WithArgumentList(
                                ArgumentList(
                                    SingletonSeparatedList<ArgumentSyntax>(
                                        Argument(
                                            SimpleLambdaExpression(
                                                    Parameter(
                                                        Identifier("x")))
                                                .WithExpressionBody(
                                                    MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        IdentifierName("x"),
                                                        IdentifierName(x.PropertyIdentifier)))))))))
                    )
                );

            return statementSyntaxList;
        }

        private List<StatementSyntax> GetSpecificationConstructorBody()
        {
            var statementSyntaxList = new List<StatementSyntax>();
            _attributes.ForEach(x =>
                x.Arguments.Where(y => y.Any() && !y.Contains("INCLUDE")).ToList().ForEach(y =>
                    statementSyntaxList.Add(ExpressionStatement(
                        InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("Query"),
                                    IdentifierName("Where")))
                            .WithArgumentList(
                                ArgumentList(
                                    SingletonSeparatedList(
                                        Argument(
                                            SimpleLambdaExpression(
                                                    Parameter(
                                                        Identifier("i")))
                                                .WithExpressionBody(
                                                    BinaryExpression(
                                                        SyntaxKind.LogicalOrExpression,
                                                        PrefixUnaryExpression(
                                                            SyntaxKind.LogicalNotExpression,
                                                            MemberAccessExpression(
                                                                SyntaxKind.SimpleMemberAccessExpression,
                                                                IdentifierName(
                                                                    $"{x.PropertyIdentifier.ToCamelCase()}{_argumentFilterConstantsHelpers.GetArgumentSufix(y)}"),
                                                                IdentifierName("HasValue"))),
                                                        BinaryExpression(
                                                            _argumentFilterConstantsHelpers.FilterAndSyntaxList
                                                                .FirstOrDefault(z => z.Argument == y).SyntaxKind,
                                                            MemberAccessExpression(
                                                                SyntaxKind.SimpleMemberAccessExpression,
                                                                IdentifierName("i"),
                                                                IdentifierName(x.PropertyIdentifier)),
                                                            IdentifierName(
                                                                $"{x.PropertyIdentifier.ToCamelCase()}{_argumentFilterConstantsHelpers.GetArgumentSufix(y)}"))))))))))
                )
            );

            return statementSyntaxList;
        }

        private IEnumerable<SyntaxNodeOrToken> GetSpecificationsConstructorParameters()
        {
            var syntaxNodeOrTokenList = new List<SyntaxNodeOrToken>();
            _attributes.ForEach(x =>
                x.Arguments.Where(y => y.Any() && !y.Contains("INCLUDE")).ToList().ForEach(y =>
                {
                    syntaxNodeOrTokenList.Add(
                        Parameter(
                                Identifier(
                                    $"{x.PropertyIdentifier.ToCamelCase()}{_argumentFilterConstantsHelpers.GetArgumentSufix(y)}"))
                            .WithType(NullableType(
                                IdentifierName(x.Type)))
                    );
                    syntaxNodeOrTokenList.Add(
                        Token(SyntaxKind.CommaToken)
                    );
                }));
            syntaxNodeOrTokenList.RemoveAt(syntaxNodeOrTokenList.Count - 1);
            return syntaxNodeOrTokenList;
        }

        private IEnumerable<SyntaxNodeOrToken> GetPaginatedSpecificationsConstructorParameters()
        {
            var syntaxNodeOrTokenList = new List<SyntaxNodeOrToken>();
            syntaxNodeOrTokenList.Add(Parameter(
                    Identifier("skip"))
                .WithType(
                    PredefinedType(
                        Token(SyntaxKind.IntKeyword))));
            syntaxNodeOrTokenList.Add(Token(SyntaxKind.CommaToken));
            syntaxNodeOrTokenList.Add(Parameter(
                    Identifier("take"))
                .WithType(
                    PredefinedType(
                        Token(SyntaxKind.IntKeyword))));
            syntaxNodeOrTokenList.Add(Token(SyntaxKind.CommaToken));
            syntaxNodeOrTokenList.AddRange(GetSpecificationsConstructorParameters().ToList());
            return syntaxNodeOrTokenList;
        }
    }
}