using Generator.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Generator.Helpers
{
    internal class EndpointSyntaxGeneratorHelper
    {
        public Dictionary<string, string> EndpointMethods = new Dictionary<string, string>() {
            {"Create", "Post"},
            {"Update", "Put"},
            {"Delete", "Delete"},
            {"GetById", "Get"},
            {"ListPaged", "Get"},
            {"Dto", "Dto"}
        };
        public SyntaxList<MemberDeclarationSyntax> GenerateResponseClassMembers(string endpoint, string modelClassName)
        {
            var members = new List<MemberDeclarationSyntax>() {
                            ConstructorDeclaration(
                                Identifier($"{endpoint}{modelClassName}Response"))
                            .WithModifiers(
                                TokenList(
                                    Token(SyntaxKind.PublicKeyword)))
                            .WithParameterList(
                                ParameterList(
                                    SingletonSeparatedList(
                                        Parameter(
                                            Identifier("correlationId"))
                                        .WithType(
                                            IdentifierName("Guid")))))
                            .WithInitializer(
                                ConstructorInitializer(
                                    SyntaxKind.BaseConstructorInitializer,
                                    ArgumentList(
                                        SingletonSeparatedList(
                                            Argument(
                                                IdentifierName("correlationId"))))))
                            .WithBody(
                                Block()),
                            ConstructorDeclaration(
                                Identifier($"{endpoint}{modelClassName}Response"))
                            .WithModifiers(
                                TokenList(
                                    Token(SyntaxKind.PublicKeyword)))
                            .WithBody(
                                Block())
            };

            if (endpoint.Equals("Create") || endpoint.Equals("Update") || endpoint.Equals("GetById"))
            {
                members.Add(
                        PropertyDeclaration(
                            IdentifierName($"{modelClassName}Dto"),
                            Identifier($"{modelClassName}"))
                        .WithModifiers(
                            TokenList(
                                Token(SyntaxKind.PublicKeyword)))
                        .WithAccessorList(
                            AccessorList(
                                List(
                                    new AccessorDeclarationSyntax[]{
                                            AccessorDeclaration(
                                                SyntaxKind.GetAccessorDeclaration)
                                            .WithSemicolonToken(
                                                Token(SyntaxKind.SemicolonToken)),
                                            AccessorDeclaration(
                                                SyntaxKind.SetAccessorDeclaration)
                                            .WithSemicolonToken(
                                                Token(SyntaxKind.SemicolonToken))}))
                ));

            }

            if (endpoint.Equals("Delete"))
                members.Add(
                    PropertyDeclaration(
            PredefinedType(
                Token(SyntaxKind.StringKeyword)),
            Identifier("Status"))
        .WithModifiers(
            TokenList(
                Token(SyntaxKind.PublicKeyword)))
        .WithAccessorList(
            AccessorList(
                List(
                    new AccessorDeclarationSyntax[]{
                        AccessorDeclaration(
                            SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(
                            Token(SyntaxKind.SemicolonToken)),
                        AccessorDeclaration(
                            SyntaxKind.SetAccessorDeclaration)
                        .WithSemicolonToken(
                            Token(SyntaxKind.SemicolonToken))})))
        .WithInitializer(
            EqualsValueClause(
                LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    Literal("Deleted"))))
        .WithSemicolonToken(
            Token(SyntaxKind.SemicolonToken))
                );

            if (endpoint.Equals("ListPaged"))
                members.AddRange(new MemberDeclarationSyntax[]{
            PropertyDeclaration(
                GenericName(
                    Identifier("List"))
                .WithTypeArgumentList(
                    TypeArgumentList(
                        SingletonSeparatedList<TypeSyntax>(
                            IdentifierName($"{modelClassName}Dto")))),
                Identifier($"{modelClassName}s"))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithAccessorList(
                AccessorList(
                    List(
                        new AccessorDeclarationSyntax[]{
                            AccessorDeclaration(
                                SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(
                                Token(SyntaxKind.SemicolonToken)),
                            AccessorDeclaration(
                                SyntaxKind.SetAccessorDeclaration)
                            .WithSemicolonToken(
                                Token(SyntaxKind.SemicolonToken))})))
            .WithInitializer(
                EqualsValueClause(
                    ObjectCreationExpression(
                        GenericName(
                            Identifier("List"))
                        .WithTypeArgumentList(
                            TypeArgumentList(
                                SingletonSeparatedList<TypeSyntax>(
                                    IdentifierName($"{modelClassName}Dto")))))
                    .WithArgumentList(
                        ArgumentList())))
            .WithSemicolonToken(
                Token(SyntaxKind.SemicolonToken)),
            PropertyDeclaration(
                PredefinedType(
                    Token(SyntaxKind.IntKeyword)),
                Identifier("PageCount"))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithAccessorList(
                AccessorList(
                    List(
                        new AccessorDeclarationSyntax[]{
                            AccessorDeclaration(
                                SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(
                                Token(SyntaxKind.SemicolonToken)),
                            AccessorDeclaration(
                                SyntaxKind.SetAccessorDeclaration)
                            .WithSemicolonToken(
                                Token(SyntaxKind.SemicolonToken))})))});
            return List(members);
        }

        public SeparatedSyntaxList<AttributeArgumentSyntax> GenerateAttributes()
        {
            return
                SeparatedList<AttributeArgumentSyntax>(
                    new SyntaxNodeOrToken[]
                    {
                        AttributeArgument(
                                LiteralExpression(
                                    SyntaxKind.StringLiteralExpression,
                                    Literal("Administrators")))
                            .WithNameEquals(
                                NameEquals(
                                    IdentifierName("Roles"))),
                        Token(SyntaxKind.CommaToken),
                        AttributeArgument(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("JwtBearerDefaults"),
                                    IdentifierName("AuthenticationScheme")))
                            .WithNameEquals(
                                NameEquals(
                                    IdentifierName("AuthenticationSchemes")))
                    });
        }

        public SyntaxList<AttributeListSyntax> GenerateSwagger(string endpoint, string modelClassName)
        {
            return List(
                    new[]
                    {
                        AttributeList(
                            SingletonSeparatedList(
                                Attribute(
                                        IdentifierName($"Http{EndpointMethods[endpoint]}"))
                                    .WithArgumentList(
                                        AttributeArgumentList(
                                            SingletonSeparatedList(
                                                AttributeArgument(
                                                    LiteralExpression(
                                                        SyntaxKind.StringLiteralExpression,
                                                        Literal(
                                                            $"api/{modelClassName.ToLower()}{(endpoint.Contains("Delete") || endpoint.Contains("GetById") ? "/{Id}" : null)}")))))))),
                        AttributeList(
                            SingletonSeparatedList(
                                Attribute(
                                        IdentifierName("SwaggerOperation"))
                                    .WithArgumentList(
                                        AttributeArgumentList(
                                            SeparatedList<AttributeArgumentSyntax>(
                                                new SyntaxNodeOrToken[]
                                                {
                                                    AttributeArgument(
                                                            LiteralExpression(
                                                                SyntaxKind
                                                                    .StringLiteralExpression,
                                                                Literal(
                                                                    $"{endpoint} {modelClassName}")))
                                                        .WithNameEquals(
                                                            NameEquals(
                                                                IdentifierName("Summary"))),
                                                    Token(SyntaxKind.CommaToken),
                                                    AttributeArgument(
                                                            LiteralExpression(
                                                                SyntaxKind
                                                                    .StringLiteralExpression,
                                                                Literal(
                                                                    $"{endpoint} {modelClassName}")))
                                                        .WithNameEquals(
                                                            NameEquals(
                                                                IdentifierName("Description"))),
                                                    Token(SyntaxKind.CommaToken),
                                                    AttributeArgument(
                                                            LiteralExpression(
                                                                SyntaxKind
                                                                    .StringLiteralExpression,
                                                                Literal(
                                                                    $"{modelClassName.ToLower()}.{endpoint.ToLower()}")))
                                                        .WithNameEquals(
                                                            NameEquals(
                                                                IdentifierName("OperationId"))),
                                                    Token(SyntaxKind.CommaToken),
                                                    AttributeArgument(
                                                            ImplicitArrayCreationExpression(
                                                                InitializerExpression(
                                                                    SyntaxKind
                                                                        .ArrayInitializerExpression,
                                                                    SingletonSeparatedList<
                                                                        ExpressionSyntax>(
                                                                        LiteralExpression(
                                                                            SyntaxKind
                                                                                .StringLiteralExpression,
                                                                            Literal(
                                                                                $"{modelClassName}Endpoints"))))))
                                                        .WithNameEquals(
                                                            NameEquals(
                                                                IdentifierName("Tags")))
                                                })))))
                    });
        }

        internal SyntaxNodeOrToken GenerateHandleParameters(string endpoint, string modelClassName)
        {

            if (endpoint.Equals("Delete") || endpoint.Equals("GetById"))
                return
                    Parameter(Identifier("request")).WithAttributeLists(
                            SingletonList(
                                AttributeList(
                                    SingletonSeparatedList(
                                        Attribute(
                                            IdentifierName("FromRoute")))))).WithType(IdentifierName($"{endpoint}{modelClassName}Request"));

            if (endpoint.Equals("ListPaged"))
                return
                     Parameter(Identifier("request")).WithAttributeLists(
                            SingletonList(
                                AttributeList(
                                    SingletonSeparatedList(
                                        Attribute(
                                            IdentifierName("FromQuery")))))).WithType(IdentifierName($"{endpoint}{modelClassName}Request"));

            return
                Parameter(Identifier("request")).WithType(IdentifierName($"{endpoint}{modelClassName}Request"));
        }

        public SeparatedSyntaxList<BaseTypeSyntax> GenerateBaseList(string method, string modelClassName)
        {
            return
                            SingletonSeparatedList<BaseTypeSyntax>(
                                SimpleBaseType(
                                    QualifiedName(
                                        QualifiedName(
                                            IdentifierName("BaseAsyncEndpoint"),
                                            GenericName(
                                                    Identifier("WithRequest"))
                                                .WithTypeArgumentList(
                                                    TypeArgumentList(
                                                        SingletonSeparatedList<TypeSyntax>(
                                                            IdentifierName($"{method}{modelClassName}Request"))))),
                                        GenericName(
                                                Identifier("WithResponse"))
                                            .WithTypeArgumentList(
                                                TypeArgumentList(
                                                    SingletonSeparatedList<TypeSyntax>(
                                                        IdentifierName($"{method}{modelClassName}Response")))))));
        }

        public SyntaxList<MemberDeclarationSyntax> GenerateRequestOrDtoClassMemeberProperties(string endpoint, List<PropertiesWithInfo> propertiesWithInfo, string modelClassName = null, List<AttributesWithInfo> attributesWithInfo = null, ArgumentFilterConstantsHelpers argumentFilterConstantsHelpers = null)
        {
            var list = new List<MemberDeclarationSyntax>();

            if (endpoint.Equals("Create") || endpoint.Equals("Update") || endpoint.Equals("Dto"))
                propertiesWithInfo.Where(x => x.Method.Equals(EndpointMethods[endpoint])).ToList().ForEach(x => list.Add(
                    PropertyDeclaration(
                            IdentifierName(x.Type),
                            Identifier(x.Identifier))
                        .WithModifiers(
                            TokenList(
                                Token(SyntaxKind.PublicKeyword)))
                        .WithAccessorList(
                            AccessorList(
                                List(
                                    new AccessorDeclarationSyntax[]{
                                    AccessorDeclaration(
                                            SyntaxKind.GetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken)),
                                    AccessorDeclaration(
                                            SyntaxKind.SetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken))})))));

            if (endpoint.Equals("GetById") || endpoint.Equals("Delete"))
                list.Add(PropertyDeclaration(
            PredefinedType(
                Token(SyntaxKind.IntKeyword)),
            Identifier($"{modelClassName}Id"))
        .WithModifiers(
            TokenList(
                Token(SyntaxKind.PublicKeyword)))
        .WithAccessorList(
            AccessorList(
                List(
                    new AccessorDeclarationSyntax[]{
                        AccessorDeclaration(
                            SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(
                            Token(SyntaxKind.SemicolonToken)),
                        AccessorDeclaration(
                            SyntaxKind.SetAccessorDeclaration)
                        .WithSemicolonToken(
                            Token(SyntaxKind.SemicolonToken))}))));


            if (endpoint.Equals("ListPaged"))
            {
                list.Add(
                    PropertyDeclaration(
                        IdentifierName("int"),
                        Identifier($"PageIndex"))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithAccessorList(
                        AccessorList(
                            List(
                                new AccessorDeclarationSyntax[]{
                                    AccessorDeclaration(
                                            SyntaxKind.GetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken)),
                                    AccessorDeclaration(
                                            SyntaxKind.SetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken))}))));
                list.Add(
    PropertyDeclaration(
        IdentifierName("int"),
        Identifier($"PageSize"))
    .WithModifiers(
        TokenList(
            Token(SyntaxKind.PublicKeyword)))
    .WithAccessorList(
        AccessorList(
            List(
                new AccessorDeclarationSyntax[]{
                                    AccessorDeclaration(
                                            SyntaxKind.GetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken)),
                                    AccessorDeclaration(
                                            SyntaxKind.SetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken))}))));


                attributesWithInfo.ForEach(x =>
                x.Arguments.Where(y => y.Any() && !y.Contains("INCLUDE")).ToList().ForEach(y => list.Add(
                PropertyDeclaration(
                        IdentifierName(x.Type),
                        Identifier($"{x.PropertyIdentifier}{argumentFilterConstantsHelpers.GetArgumentSufix(y)}"))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithAccessorList(
                        AccessorList(
                            List(
                                new AccessorDeclarationSyntax[]{
                                    AccessorDeclaration(
                                            SyntaxKind.GetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken)),
                                    AccessorDeclaration(
                                            SyntaxKind.SetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken))}))))
            ));
            }

            return List(list);
        }

        public IEnumerable<ExpressionSyntax> GenerateServiceRequestObjectInitialization(string endpoint, List<KeyValuePair<string, string>> propertiesWithAttributes)
        {
            var properties = propertiesWithAttributes.Where(x => x.Value == EndpointMethods[endpoint]).Select(x => x.Key).ToList();
            var list = new List<SyntaxNodeOrToken>();
            properties.ForEach(x =>
                {
                    list.Add(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName(x),
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("request"),
                                IdentifierName(x))));
                    list.Add(
                        Token(SyntaxKind.CommaToken));
                }
            );
            // Remove last comma
            list.RemoveAt(list.Count - 1);
            var initializerExpression = SeparatedList<ExpressionSyntax>(list);
            return initializerExpression;
        }

        internal SeparatedSyntaxList<ArgumentSyntax> GenerateListPagedSpecificationRequest(List<AttributesWithInfo> attributesWithInfo, ArgumentFilterConstantsHelpers argumentFilterConstantsHelpers, bool pagedSpec = false)
        {
            var arguments = new List<SyntaxNodeOrToken>();
            if (pagedSpec)
            {
                arguments.AddRange(new SyntaxNodeOrToken[]{
                                                Argument(
                                                    BinaryExpression(
                                                        SyntaxKind.MultiplyExpression,
                                                        MemberAccessExpression(
                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                            IdentifierName("request"),
                                                            IdentifierName("PageIndex")),
                                                        MemberAccessExpression(
                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                            IdentifierName("request"),
                                                            IdentifierName("PageSize")))),
                                                Token(SyntaxKind.CommaToken),
                                                Argument(
                                                    MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        IdentifierName("request"),
                                                        IdentifierName("PageSize"))),
                                                Token(SyntaxKind.CommaToken)});
            }
            attributesWithInfo.ForEach(x =>

                    x.Arguments.Where(y => y.Any() && !y.Contains("INCLUDE")).ToList().ForEach(y =>
                    {
                        arguments.Add(
                            Argument(
                            MemberAccessExpression(
                                                                SyntaxKind.SimpleMemberAccessExpression,
                                                                IdentifierName("request"),
                                                                IdentifierName($"{x.PropertyIdentifier}{argumentFilterConstantsHelpers.GetArgumentSufix(y)}"))));
                        arguments.Add(
                            Token(SyntaxKind.CommaToken));
                    }
            ));
            // Remove last comma
            arguments.RemoveAt(arguments.Count - 1);
            return SeparatedList<ArgumentSyntax>(arguments);
        }
    }
}