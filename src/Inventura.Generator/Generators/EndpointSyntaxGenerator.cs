using System.Collections.Generic;
using Generator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Generator.Generators
{
    internal class EndpointSyntaxGenerator
    {
        private readonly ArgumentFilterConstantsHelpers _argumentFilterConstantsHelpers =
            new();

        private List<AttributesWithInfo> _attributesWithInfo;
        private string _modelClassName;
        private List<KeyValuePair<string, string>> _propertiesWithAttributes;

        private List<PropertiesWithInfo> _propertiesWithInfo;
        private string _serviceInstanceName;
        private string _serviceInterfaceName;
        private string _serviceParameterName;
        private readonly EndpointSyntaxGeneratorHelper helper = new();

        public SyntaxNode GenerateEndpointsNode(string modelClassName,
            List<KeyValuePair<string, string>> propertiesWithAttributes,
            List<PropertiesWithInfo> propertiesWithInfo,
            List<AttributesWithInfo> attributesWithInfo)
        {
            _modelClassName = modelClassName;
            _serviceInterfaceName = $"I{modelClassName}Service";
            _serviceInstanceName = $"_{modelClassName.ToCamelCase()}Service";
            _serviceParameterName = $"{modelClassName.ToCamelCase()}Service";
            _propertiesWithAttributes = propertiesWithAttributes;
            _propertiesWithInfo = propertiesWithInfo;
            _attributesWithInfo = attributesWithInfo;

            return GenerateEndpointsNode();
        }

        private SyntaxNode GenerateEndpointsNode()
        {
            return
                CompilationUnit()
                    .WithMembers(SingletonList(GenerateMembers())).NormalizeWhitespace();
        }

        private MemberDeclarationSyntax GenerateMembers()
        {
            return NamespaceDeclaration(
                QualifiedName(
                    QualifiedName(
                        IdentifierName("PublicApi"),
                        IdentifierName("Endpoints")),
                    IdentifierName($"{_modelClassName}Endpoints"))).WithMembers(List(
                GetMembers()
            ));
        }

        private List<MemberDeclarationSyntax> GetMembers()
        {
            var list = new List<MemberDeclarationSyntax>();
            list.AddRange(
                GenerateEndpoint("Create"));
            list.AddRange(GenerateEndpoint("Update"));
            list.AddRange(GenerateEndpoint("GetById"));
            list.AddRange(GenerateEndpoint("Delete"));
            list.AddRange(GenerateEndpoint("ListPaged"));
            list.AddRange(
                GenerateDto());
            return list;
        }

        private IEnumerable<MemberDeclarationSyntax> GenerateDto()
        {
            return new List<MemberDeclarationSyntax>
            {
                ClassDeclaration($"{_modelClassName}Dto")
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithMembers(helper.GenerateRequestOrDtoClassMemeberProperties("Dto", _propertiesWithInfo))
            };
        }

        private IEnumerable<MemberDeclarationSyntax> GenerateEndpoint(string endpoint)
        {
            return new List<MemberDeclarationSyntax>
            {
                ClassDeclaration(endpoint)
                    .WithAttributeLists(
                        SingletonList(
                            AttributeList(
                                SingletonSeparatedList(
                                    Attribute(
                                            IdentifierName("Authorize"))
                                        .WithArgumentList(
                                            AttributeArgumentList(helper.GenerateAttributes()))))))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithBaseList(
                        BaseList(helper.GenerateBaseList(endpoint, _modelClassName)))
                    .WithMembers(
                        List(
                            new MemberDeclarationSyntax[]
                            {
                                FieldDeclaration(
                                        VariableDeclaration(
                                                IdentifierName(_serviceInterfaceName))
                                            .WithVariables(
                                                SingletonSeparatedList(
                                                    VariableDeclarator(
                                                        Identifier(_serviceInstanceName)))))
                                    .WithModifiers(
                                        TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword))),
                                FieldDeclaration(
                                        VariableDeclaration(
                                                IdentifierName("IMapper"))
                                            .WithVariables(
                                                SingletonSeparatedList(
                                                    VariableDeclarator(
                                                        Identifier("_mapper")))))
                                    .WithModifiers(
                                        TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword))),
                                ConstructorDeclaration(
                                        Identifier(endpoint))
                                    .WithModifiers(
                                        TokenList(
                                            Token(SyntaxKind.PublicKeyword)))
                                    .WithParameterList(
                                        ParameterList(
                                            SeparatedList<ParameterSyntax>(
                                                new SyntaxNodeOrToken[]
                                                {
                                                    Parameter(
                                                            Identifier(_serviceParameterName))
                                                        .WithType(
                                                            IdentifierName(_serviceInterfaceName)),
                                                    Token(SyntaxKind.CommaToken),
                                                    Parameter(
                                                            Identifier("mapper"))
                                                        .WithType(
                                                            IdentifierName("IMapper"))
                                                })))
                                    .WithBody(
                                        Block(
                                            ExpressionStatement(
                                                AssignmentExpression(
                                                    SyntaxKind.SimpleAssignmentExpression,
                                                    IdentifierName(_serviceInstanceName),
                                                    IdentifierName(_serviceParameterName))),
                                            ExpressionStatement(
                                                AssignmentExpression(
                                                    SyntaxKind.SimpleAssignmentExpression,
                                                    IdentifierName("_mapper"),
                                                    IdentifierName("mapper"))))),
                                MethodDeclaration(
                                        GenericName(
                                                Identifier("Task"))
                                            .WithTypeArgumentList(
                                                TypeArgumentList(
                                                    SingletonSeparatedList<TypeSyntax>(
                                                        GenericName(
                                                                Identifier("ActionResult"))
                                                            .WithTypeArgumentList(
                                                                TypeArgumentList(
                                                                    SingletonSeparatedList<TypeSyntax>(
                                                                        IdentifierName(
                                                                            $" {endpoint}{_modelClassName}Response"))))))),
                                        Identifier("HandleAsync"))
                                    .WithAttributeLists(helper.GenerateSwagger(endpoint, _modelClassName))
                                    .WithModifiers(
                                        TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.OverrideKeyword),
                                            Token(SyntaxKind.AsyncKeyword)))
                                    .WithParameterList(
                                        ParameterList(
                                            SeparatedList<ParameterSyntax>(
                                                new[]
                                                {
                                                    helper.GenerateHandleParameters(endpoint, _modelClassName),
                                                    Token(SyntaxKind.CommaToken),
                                                    Parameter(
                                                            Identifier("cancellationToken"))
                                                        .WithType(
                                                            IdentifierName("CancellationToken"))
                                                })))
                                    .WithBody(GenerateHandleBody(endpoint)
                                    )
                            })),
                ClassDeclaration($"{endpoint}{_modelClassName}Request")
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithBaseList(
                        BaseList(
                            SingletonSeparatedList<BaseTypeSyntax>(
                                SimpleBaseType(
                                    IdentifierName("BaseRequest")))))
                    .WithMembers(helper.GenerateRequestOrDtoClassMemeberProperties(endpoint, _propertiesWithInfo,
                        _modelClassName, _attributesWithInfo, _argumentFilterConstantsHelpers)),
                ClassDeclaration($"{endpoint}{_modelClassName}Response")
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithBaseList(
                        BaseList(
                            SingletonSeparatedList<BaseTypeSyntax>(
                                SimpleBaseType(
                                    IdentifierName("BaseResponse")))))
                    .WithMembers(helper.GenerateResponseClassMembers(endpoint, _modelClassName))
            };
        }

        private BlockSyntax GenerateHandleBody(string endpoint)
        {
            var statements = new List<StatementSyntax>
            {
                LocalDeclarationStatement(
                    VariableDeclaration(
                            IdentifierName(
                                Identifier(
                                    TriviaList(),
                                    SyntaxKind.VarKeyword,
                                    "var",
                                    "var",
                                    TriviaList())))
                        .WithVariables(
                            SingletonSeparatedList(
                                VariableDeclarator(
                                        Identifier("response"))
                                    .WithInitializer(
                                        EqualsValueClause(
                                            ObjectCreationExpression(
                                                    IdentifierName(
                                                        $"{endpoint}{_modelClassName}Response"))
                                                .WithArgumentList(
                                                    ArgumentList(
                                                        SingletonSeparatedList(
                                                            Argument(
                                                                InvocationExpression(
                                                                    MemberAccessExpression(
                                                                        SyntaxKind
                                                                            .SimpleMemberAccessExpression,
                                                                        IdentifierName(
                                                                            "request"),
                                                                        IdentifierName(
                                                                            "CorrelationId"))))))))))))
            };
            statements.AddRange(GenerateHandleBodyMethodSpecificStatements(endpoint)
            );
            return Block(statements);
        }

        private List<StatementSyntax> GenerateHandleBodyMethodSpecificStatements(string endpoint)
        {
            var statements = new List<StatementSyntax>();

            if (endpoint.Equals("Create") || endpoint.Equals("Update"))
                statements.Add(
                    LocalDeclarationStatement(
                        VariableDeclaration(
                                IdentifierName(
                                    Identifier(
                                        TriviaList(),
                                        SyntaxKind.VarKeyword,
                                        "var",
                                        "var",
                                        TriviaList())))
                            .WithVariables(
                                SingletonSeparatedList(
                                    VariableDeclarator(
                                            Identifier($"{_modelClassName.ToCamelCase()}"))
                                        .WithInitializer(
                                            EqualsValueClause(
                                                AwaitExpression(
                                                    InvocationExpression(
                                                            MemberAccessExpression(
                                                                SyntaxKind
                                                                    .SimpleMemberAccessExpression,
                                                                IdentifierName(
                                                                    _serviceInstanceName),
                                                                IdentifierName(
                                                                    $"{helper.EndpointMethods[endpoint]}Async")))
                                                        .WithArgumentList(
                                                            ArgumentList(
                                                                SingletonSeparatedList(
                                                                    Argument(
                                                                        ObjectCreationExpression(
                                                                                IdentifierName(
                                                                                    $"{_modelClassName}"))
                                                                            .WithInitializer(
                                                                                InitializerExpression(
                                                                                    SyntaxKind
                                                                                        .ObjectInitializerExpression,
                                                                                    SeparatedList(
                                                                                        helper
                                                                                            .GenerateServiceRequestObjectInitialization(
                                                                                                endpoint,
                                                                                                _propertiesWithAttributes)
                                                                                    )))))))))))))
                );


            if (endpoint.Equals("GetById") || endpoint.Equals("Delete"))
                statements.Add(
                    LocalDeclarationStatement(
                        VariableDeclaration(
                                IdentifierName(
                                    Identifier(
                                        TriviaList(),
                                        SyntaxKind.VarKeyword,
                                        "var",
                                        "var",
                                        TriviaList())))
                            .WithVariables(
                                SingletonSeparatedList(
                                    VariableDeclarator(
                                            Identifier($"{_modelClassName.ToCamelCase()}"))
                                        .WithInitializer(
                                            EqualsValueClause(
                                                AwaitExpression(
                                                    InvocationExpression(
                                                            MemberAccessExpression(
                                                                SyntaxKind.SimpleMemberAccessExpression,
                                                                IdentifierName(
                                                                    $"_{_modelClassName.ToCamelCase()}Service"),
                                                                IdentifierName(
                                                                    $"{helper.EndpointMethods[endpoint]}Async")))
                                                        .WithArgumentList(
                                                            ArgumentList(
                                                                SingletonSeparatedList(
                                                                    Argument(
                                                                        MemberAccessExpression(
                                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                                            IdentifierName("request"),
                                                                            IdentifierName("Id"))))))))))))
                );

            if (endpoint.Equals("Create") || endpoint.Equals("Update") || endpoint.Equals("GetById"))
                statements.Add(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("response"),
                            IdentifierName(_modelClassName)),
                        InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("_mapper"),
                                    GenericName(
                                            Identifier("Map"))
                                        .WithTypeArgumentList(
                                            TypeArgumentList(
                                                SingletonSeparatedList<TypeSyntax>(
                                                    IdentifierName($"{_modelClassName}Dto"))))))
                            .WithArgumentList(
                                ArgumentList(
                                    SingletonSeparatedList(
                                        Argument(
                                            IdentifierName($"{_modelClassName.ToCamelCase()}"))))))));


            if (endpoint.Equals("ListPaged"))
            {
                statements.Add(LocalDeclarationStatement(
                    VariableDeclaration(
                            IdentifierName(
                                Identifier(
                                    TriviaList(),
                                    SyntaxKind.VarKeyword,
                                    "var",
                                    "var",
                                    TriviaList())))
                        .WithVariables(
                            SingletonSeparatedList(
                                VariableDeclarator(
                                        Identifier("filterSpec"))
                                    .WithInitializer(
                                        EqualsValueClause(
                                            ObjectCreationExpression(
                                                    IdentifierName($"{_modelClassName}FilterSpecification"))
                                                .WithArgumentList(
                                                    ArgumentList(
                                                        helper.GenerateListPagedSpecificationRequest(
                                                            _attributesWithInfo, _argumentFilterConstantsHelpers)
                                                    ))))))));
                statements.Add(LocalDeclarationStatement(
                    VariableDeclaration(
                            IdentifierName(
                                Identifier(
                                    TriviaList(),
                                    SyntaxKind.VarKeyword,
                                    "var",
                                    "var",
                                    TriviaList())))
                        .WithVariables(
                            SingletonSeparatedList(
                                VariableDeclarator(
                                        Identifier("pagedSpec"))
                                    .WithInitializer(
                                        EqualsValueClause(
                                            ObjectCreationExpression(
                                                    IdentifierName($"{_modelClassName}FilterPaginatedSpecification"))
                                                .WithArgumentList(
                                                    ArgumentList(
                                                        helper.GenerateListPagedSpecificationRequest(
                                                            _attributesWithInfo, _argumentFilterConstantsHelpers, true)
                                                    ))))))));
                statements.Add(LocalDeclarationStatement(
                    VariableDeclaration(
                            IdentifierName(
                                Identifier(
                                    TriviaList(),
                                    SyntaxKind.VarKeyword,
                                    "var",
                                    "var",
                                    TriviaList())))
                        .WithVariables(
                            SingletonSeparatedList(
                                VariableDeclarator(
                                        Identifier($"{_modelClassName.ToCamelCase()}s"))
                                    .WithInitializer(
                                        EqualsValueClause(
                                            AwaitExpression(
                                                InvocationExpression(
                                                        MemberAccessExpression(
                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                            IdentifierName(_serviceInstanceName),
                                                            IdentifierName("GetAsync")))
                                                    .WithArgumentList(
                                                        ArgumentList(
                                                            SeparatedList<ArgumentSyntax>(
                                                                new SyntaxNodeOrToken[]
                                                                {
                                                                    Argument(
                                                                        IdentifierName("filterSpec")),
                                                                    Token(SyntaxKind.CommaToken),
                                                                    Argument(
                                                                        IdentifierName("pagedSpec"))
                                                                }))))))))));
                statements.Add(ExpressionStatement(
                    InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("response"),
                                    IdentifierName($"{_modelClassName}s")),
                                IdentifierName("AddRange")))
                        .WithArgumentList(
                            ArgumentList(
                                SingletonSeparatedList(
                                    Argument(
                                        InvocationExpression(
                                                MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        IdentifierName($"{_modelClassName.ToCamelCase()}s"),
                                                        IdentifierName("List")),
                                                    IdentifierName("Select")))
                                            .WithArgumentList(
                                                ArgumentList(
                                                    SingletonSeparatedList(
                                                        Argument(
                                                            MemberAccessExpression(
                                                                SyntaxKind.SimpleMemberAccessExpression,
                                                                IdentifierName("_mapper"),
                                                                GenericName(
                                                                        Identifier("Map"))
                                                                    .WithTypeArgumentList(
                                                                        TypeArgumentList(
                                                                            SingletonSeparatedList<TypeSyntax>(
                                                                                IdentifierName(
                                                                                    $"{_modelClassName}Dto")))))))))))))));
                statements.Add(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("response"),
                            IdentifierName("PageCount")),
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName($"{_modelClassName.ToCamelCase()}s"),
                            IdentifierName("Count")))));
            }

            if (endpoint.Equals("Create") || endpoint.Equals("Update"))
                statements.Add(ReturnStatement(IdentifierName("response")));

            else
                statements.Add(ReturnStatement(
                    InvocationExpression(
                            IdentifierName("Ok"))
                        .WithArgumentList(
                            ArgumentList(
                                SingletonSeparatedList(
                                    Argument(
                                        IdentifierName("response")))))));


            return statements;
        }
    }
}