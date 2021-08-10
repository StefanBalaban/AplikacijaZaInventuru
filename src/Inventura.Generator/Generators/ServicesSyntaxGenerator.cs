using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Generator.Generators
{
    public class ServicesSyntaxGenerator
    {
        private readonly AdhocWorkspace _workspace = new();
        private SyntaxGenerator _generator;
        private string _modelClassName;
        private List<KeyValuePair<string, string>> _propertiesWithAttributes = new();
        private string _repositoryFieldName;

        public SyntaxNode GenerateServiceClassNode(string modelClassName,
            List<KeyValuePair<string, string>> propertiesWithAttributes)
        {
            _propertiesWithAttributes = propertiesWithAttributes;
            _modelClassName = modelClassName;
            _repositoryFieldName = $"_{_modelClassName.ToCamelCase()}Repository";
            _generator = SyntaxGenerator.GetGenerator(_workspace, LanguageNames.CSharp);

            var usings = GenerateUsings();
            var fields = GenerateFields();
            var constructor = GenerateConstructor();

            var getAsyncMethod = GenerateGetAsyncMethod();
            var getPagedMethod = GenerateGetPagedAsyncMethod();
            var getByIdAsyncMethod = GenerateGetByIdAsyncMethod();
            var postAsyncMethod = GeneratePostAsyncMethod();
            var putAsyncMethod = GeneratePutAsyncMethod();
            var deleteAsyncMethod = GenerateDeleteAsyncMethod();

            var members = new[]
            {
                fields[0],
                fields[1],
                constructor,
                getAsyncMethod,
                getByIdAsyncMethod,
                getPagedMethod,
                postAsyncMethod,
                putAsyncMethod,
                deleteAsyncMethod
            };

            var classDefinition = _generator.ClassDeclaration($"{_modelClassName}Service", null, Accessibility.Public,
                DeclarationModifiers.None,
                members: members);

            return
                CompilationUnit()
                    .WithUsings(List(usings))
                    .WithMembers(SingletonList<MemberDeclarationSyntax>(
                        NamespaceDeclaration(IdentifierName("ApplicationCore.Services"))
                            .WithMembers(SingletonList(classDefinition))))
                    .NormalizeWhitespace();
        }

        private UsingDirectiveSyntax[] GenerateUsings()
        {
            return
                new[]
                {
                    UsingDirective(
                        QualifiedName(
                            IdentifierName("Ardalis"),
                            IdentifierName("Specification"))),
                    UsingDirective(
                        IdentifierName("ApplicationCore.Interfaces")),
                    UsingDirective(
                        IdentifierName("ApplicationCore.Extensions")),
                    UsingDirective(
                        IdentifierName("ApplicationCore.Entities"))
                };
        }

        private SyntaxNode[] GenerateFields()
        {
            return new[]
            {
                FieldDeclaration(
                        VariableDeclaration(
                                GenericName(
                                        Identifier("IAsyncRepository"))
                                    .WithTypeArgumentList(
                                        TypeArgumentList(
                                            SingletonSeparatedList<TypeSyntax>(
                                                IdentifierName(_modelClassName)))))
                            .WithVariables(
                                SingletonSeparatedList(
                                    VariableDeclarator(
                                        Identifier(_repositoryFieldName)))))
                    .WithModifiers(
                        TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword))),
                FieldDeclaration(
                        VariableDeclaration(
                                GenericName(
                                        Identifier("IAppLogger"))
                                    .WithTypeArgumentList(
                                        TypeArgumentList(
                                            SingletonSeparatedList<TypeSyntax>(
                                                IdentifierName($"{_modelClassName}Service")))))
                            .WithVariables(
                                SingletonSeparatedList(
                                    VariableDeclarator(
                                        Identifier("_logger")))))
                    .WithModifiers(
                        TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)))
            };
        }

        private SyntaxNode GenerateConstructor()
        {
            return
                ConstructorDeclaration(
                        Identifier($"{_modelClassName}Services"))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SeparatedList<ParameterSyntax>(
                                new SyntaxNodeOrToken[]
                                {
                                    Parameter(
                                            Identifier($"{_modelClassName.ToCamelCase()}Repository"))
                                        .WithType(
                                            GenericName(
                                                    Identifier("IAsyncRepository"))
                                                .WithTypeArgumentList(
                                                    TypeArgumentList(
                                                        SingletonSeparatedList<TypeSyntax>(
                                                            IdentifierName(_modelClassName))))),
                                    Token(SyntaxKind.CommaToken),
                                    Parameter(
                                            Identifier("logger"))
                                        .WithType(
                                            GenericName(
                                                    Identifier("IAppLogger"))
                                                .WithTypeArgumentList(
                                                    TypeArgumentList(
                                                        SingletonSeparatedList<TypeSyntax>(
                                                            IdentifierName($"{_modelClassName}Service")))))
                                })))
                    .WithBody(
                        Block(
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    IdentifierName(_repositoryFieldName),
                                    IdentifierName($"{_modelClassName.ToCamelCase()}Repository"))),
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    IdentifierName("_logger"),
                                    IdentifierName("logger"))))).NormalizeWhitespace();
        }

        private SyntaxNode GenerateDeleteAsyncMethod()
        {
            return
                MethodDeclaration(
                        GenericName(
                                Identifier("Task"))
                            .WithTypeArgumentList(
                                TypeArgumentList(
                                    SingletonSeparatedList<TypeSyntax>(
                                        PredefinedType(
                                            Token(SyntaxKind.BoolKeyword))))),
                        Identifier("DeleteAsync"))
                    .WithModifiers(
                        TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.AsyncKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SingletonSeparatedList(
                                Parameter(
                                        Identifier("id"))
                                    .WithType(
                                        PredefinedType(
                                            Token(SyntaxKind.IntKeyword))))))
                    .WithBody(
                        Block(
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
                                                    Identifier(_modelClassName.ToCamelCase()))
                                                .WithInitializer(
                                                    EqualsValueClause(
                                                        AwaitExpression(
                                                            InvocationExpression(
                                                                    MemberAccessExpression(
                                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                                        IdentifierName(_repositoryFieldName),
                                                                        IdentifierName("GetByIdAsync")))
                                                                .WithArgumentList(
                                                                    ArgumentList(
                                                                        SingletonSeparatedList(
                                                                            Argument(
                                                                                IdentifierName("id"))))))))))),
                            ExpressionStatement(
                                InvocationExpression(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                IdentifierName("Guard"),
                                                IdentifierName("Against")),
                                            IdentifierName("EntityNotFound")))
                                    .WithArgumentList(
                                        ArgumentList(
                                            SeparatedList<ArgumentSyntax>(
                                                new SyntaxNodeOrToken[]
                                                {
                                                    Argument(
                                                        IdentifierName(_modelClassName.ToCamelCase())),
                                                    Token(SyntaxKind.CommaToken),
                                                    Argument(
                                                        InvocationExpression(
                                                                IdentifierName(
                                                                    Identifier(
                                                                        TriviaList(),
                                                                        SyntaxKind.NameOfKeyword,
                                                                        "nameof",
                                                                        "nameof",
                                                                        TriviaList())))
                                                            .WithArgumentList(
                                                                ArgumentList(
                                                                    SingletonSeparatedList(
                                                                        Argument(
                                                                            IdentifierName(_modelClassName))))))
                                                })))),
                            ExpressionStatement(
                                AwaitExpression(
                                    InvocationExpression(
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                IdentifierName(_repositoryFieldName),
                                                IdentifierName("DeleteAsync")))
                                        .WithArgumentList(
                                            ArgumentList(
                                                SingletonSeparatedList(
                                                    Argument(
                                                        IdentifierName(_modelClassName.ToCamelCase()))))))),
                            ReturnStatement(
                                LiteralExpression(
                                    SyntaxKind.TrueLiteralExpression)))).NormalizeWhitespace();
        }

        private SyntaxNode GeneratePutAsyncMethod()
        {
            var properties = _propertiesWithAttributes.Where(x => x.Value == "Put").Select(x => x.Key).ToList();
            var listOfStatements = new List<StatementSyntax>
            {
                ExpressionStatement(
                    InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("Guard"),
                                    IdentifierName("Against")),
                                IdentifierName("ModelStateIsInvalid")))
                        .WithArgumentList(
                            ArgumentList(
                                SeparatedList<ArgumentSyntax>(
                                    new SyntaxNodeOrToken[]
                                    {
                                        Argument(
                                            IdentifierName("t")),
                                        Token(SyntaxKind.CommaToken),
                                        Argument(
                                            InvocationExpression(
                                                    IdentifierName(
                                                        Identifier(
                                                            TriviaList(),
                                                            SyntaxKind.NameOfKeyword,
                                                            "nameof",
                                                            "nameof",
                                                            TriviaList())))
                                                .WithArgumentList(
                                                    ArgumentList(
                                                        SingletonSeparatedList(
                                                            Argument(
                                                                IdentifierName(_modelClassName))))))
                                    })))),
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
                                        Identifier(_modelClassName.ToCamelCase()))
                                    .WithInitializer(
                                        EqualsValueClause(
                                            AwaitExpression(
                                                InvocationExpression(
                                                        MemberAccessExpression(
                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                            IdentifierName(_repositoryFieldName),
                                                            IdentifierName("GetByIdAsync")))
                                                    .WithArgumentList(
                                                        ArgumentList(
                                                            SingletonSeparatedList(
                                                                Argument(
                                                                    MemberAccessExpression(
                                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                                        IdentifierName("t"),
                                                                        IdentifierName("Id")))))))))))),
                ExpressionStatement(
                    InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("Guard"),
                                    IdentifierName("Against")),
                                IdentifierName("EntityNotFound")))
                        .WithArgumentList(
                            ArgumentList(
                                SeparatedList<ArgumentSyntax>(
                                    new SyntaxNodeOrToken[]
                                    {
                                        Argument(
                                            IdentifierName(_modelClassName.ToCamelCase())),
                                        Token(SyntaxKind.CommaToken),
                                        Argument(
                                            InvocationExpression(
                                                    IdentifierName(
                                                        Identifier(
                                                            TriviaList(),
                                                            SyntaxKind.NameOfKeyword,
                                                            "nameof",
                                                            "nameof",
                                                            TriviaList())))
                                                .WithArgumentList(
                                                    ArgumentList(
                                                        SingletonSeparatedList(
                                                            Argument(
                                                                IdentifierName(_modelClassName))))))
                                    }))))
            };
            properties.ForEach(x =>
                listOfStatements.Add(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName(_modelClassName.ToCamelCase()),
                            IdentifierName(x)),
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("t"),
                            IdentifierName(x)))))
            );
            listOfStatements.Add(
                ExpressionStatement(
                    AwaitExpression(
                        InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName($"{_repositoryFieldName}"),
                                    IdentifierName("UpdateAsync")))
                            .WithArgumentList(
                                ArgumentList(
                                    SingletonSeparatedList(
                                        Argument(
                                            IdentifierName($"{_modelClassName.ToCamelCase()}")))))))
            );
            listOfStatements.Add(
                ReturnStatement(
                    IdentifierName(_modelClassName.ToCamelCase())));

            return
                MethodDeclaration(
                        GenericName(
                                Identifier("Task"))
                            .WithTypeArgumentList(
                                TypeArgumentList(
                                    SingletonSeparatedList<TypeSyntax>(
                                        IdentifierName(_modelClassName)))),
                        Identifier("PutAsync"))
                    .WithModifiers(
                        TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.AsyncKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SingletonSeparatedList(
                                Parameter(
                                        Identifier("t"))
                                    .WithType(
                                        IdentifierName(_modelClassName)))))
                    .WithBody(
                        Block(
                            listOfStatements)).NormalizeWhitespace();
        }

        private SyntaxNode GeneratePostAsyncMethod()
        {
            var properties = _propertiesWithAttributes.Where(x => x.Value == "Post").Select(x => x.Key).ToList();
            var list = new List<SyntaxNodeOrToken>();
            properties.ForEach(x =>
                {
                    list.Add(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName(x),
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("t"),
                                IdentifierName(x))));
                    list.Add(
                        Token(SyntaxKind.CommaToken));
                }
            );
            // Remove last comma
            list.RemoveAt(list.Count - 1);
            var initializerExpression = SeparatedList<ExpressionSyntax>(list);


            return MethodDeclaration(
                    GenericName(
                            Identifier("Task"))
                        .WithTypeArgumentList(
                            TypeArgumentList(
                                SingletonSeparatedList<TypeSyntax>(
                                    IdentifierName(_modelClassName)))),
                    Identifier("PostAsync"))
                .WithModifiers(
                    TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.AsyncKeyword)))
                .WithParameterList(
                    ParameterList(
                        SingletonSeparatedList(
                            Parameter(
                                    Identifier("t"))
                                .WithType(
                                    IdentifierName(_modelClassName)))))
                .WithBody(
                    Block(
                        ExpressionStatement(
                            InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("Guard"),
                                            IdentifierName("Against")),
                                        IdentifierName("ModelStateIsInvalid")))
                                .WithArgumentList(
                                    ArgumentList(
                                        SeparatedList<ArgumentSyntax>(
                                            new SyntaxNodeOrToken[]
                                            {
                                                Argument(
                                                    IdentifierName("t")),
                                                Token(SyntaxKind.CommaToken),
                                                Argument(
                                                    InvocationExpression(
                                                            IdentifierName(
                                                                Identifier(
                                                                    TriviaList(),
                                                                    SyntaxKind.NameOfKeyword,
                                                                    "nameof",
                                                                    "nameof",
                                                                    TriviaList())))
                                                        .WithArgumentList(
                                                            ArgumentList(
                                                                SingletonSeparatedList(
                                                                    Argument(
                                                                        IdentifierName(_modelClassName))))))
                                            })))),
                        ReturnStatement(
                            AwaitExpression(
                                InvocationExpression(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName(_repositoryFieldName),
                                            IdentifierName("AddAsync")))
                                    .WithArgumentList(
                                        ArgumentList(
                                            SingletonSeparatedList(
                                                Argument(
                                                    ObjectCreationExpression(
                                                            IdentifierName(_modelClassName))
                                                        .WithInitializer(
                                                            InitializerExpression(
                                                                SyntaxKind.ObjectInitializerExpression,
                                                                initializerExpression)))))))))).NormalizeWhitespace();
        }

        private SyntaxNode GenerateGetPagedAsyncMethod()
        {
            return
                MethodDeclaration(
                        GenericName(Identifier("Task")).WithTypeArgumentList(
                            TypeArgumentList(SingletonSeparatedList<TypeSyntax>(GenericName(Identifier("ListEntity"))
                                .WithTypeArgumentList(
                                    TypeArgumentList(
                                        SingletonSeparatedList<TypeSyntax>(IdentifierName(_modelClassName))))))),
                        Identifier("GetAsync"))
                    .WithModifiers(
                        TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.AsyncKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SeparatedList<ParameterSyntax>(
                                new SyntaxNodeOrToken[]
                                {
                                    Parameter(
                                            Identifier("filterSpec"))
                                        .WithType(
                                            GenericName(
                                                    Identifier("Specification"))
                                                .WithTypeArgumentList(
                                                    TypeArgumentList(
                                                        SingletonSeparatedList<TypeSyntax>(
                                                            IdentifierName(_modelClassName))))),
                                    Token(SyntaxKind.CommaToken),
                                    Parameter(
                                            Identifier("pagedSpec"))
                                        .WithType(
                                            GenericName(
                                                    Identifier("Specification"))
                                                .WithTypeArgumentList(
                                                    TypeArgumentList(
                                                        SingletonSeparatedList<TypeSyntax>(
                                                            IdentifierName(_modelClassName)))))
                                })))
                    .WithBody(
                        Block(
                            SingletonList<StatementSyntax>(
                                ReturnStatement(
                                    ObjectCreationExpression(
                                            GenericName(
                                                    Identifier("ListEntity"))
                                                .WithTypeArgumentList(
                                                    TypeArgumentList(
                                                        SingletonSeparatedList<TypeSyntax>(
                                                            IdentifierName(_modelClassName)))))
                                        .WithInitializer(
                                            InitializerExpression(
                                                SyntaxKind.ObjectInitializerExpression,
                                                SeparatedList<ExpressionSyntax>(
                                                    new SyntaxNodeOrToken[]
                                                    {
                                                        AssignmentExpression(
                                                            SyntaxKind.SimpleAssignmentExpression,
                                                            IdentifierName("List"),
                                                            AwaitExpression(
                                                                InvocationExpression(
                                                                        MemberAccessExpression(
                                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                                            IdentifierName(_repositoryFieldName),
                                                                            IdentifierName("ListAsync")))
                                                                    .WithArgumentList(
                                                                        ArgumentList(
                                                                            SingletonSeparatedList(
                                                                                Argument(
                                                                                    IdentifierName("pagedSpec"))))))),
                                                        Token(SyntaxKind.CommaToken),
                                                        AssignmentExpression(
                                                            SyntaxKind.SimpleAssignmentExpression,
                                                            IdentifierName("Count"),
                                                            AwaitExpression(
                                                                InvocationExpression(
                                                                        MemberAccessExpression(
                                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                                            IdentifierName(_repositoryFieldName),
                                                                            IdentifierName("CountAsync")))
                                                                    .WithArgumentList(
                                                                        ArgumentList(
                                                                            SingletonSeparatedList(
                                                                                Argument(
                                                                                    IdentifierName("filterSpec")))))))
                                                    }))))))).NormalizeWhitespace();
        }

        private SyntaxNode GenerateGetByIdAsyncMethod()
        {
            return
                MethodDeclaration(
                        GenericName(
                                Identifier("Task"))
                            .WithTypeArgumentList(
                                TypeArgumentList(
                                    SingletonSeparatedList<TypeSyntax>(
                                        IdentifierName(_modelClassName)))),
                        Identifier("GetAsync"))
                    .WithModifiers(
                        TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.AsyncKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SingletonSeparatedList(
                                Parameter(
                                        Identifier("id"))
                                    .WithType(
                                        PredefinedType(
                                            Token(SyntaxKind.IntKeyword))))))
                    .WithBody(
                        Block(
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
                                                    Identifier(_modelClassName.ToCamelCase()))
                                                .WithInitializer(
                                                    EqualsValueClause(
                                                        AwaitExpression(
                                                            InvocationExpression(
                                                                    MemberAccessExpression(
                                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                                        IdentifierName(_repositoryFieldName),
                                                                        IdentifierName("GetByIdAsync")))
                                                                .WithArgumentList(
                                                                    ArgumentList(
                                                                        SingletonSeparatedList(
                                                                            Argument(
                                                                                IdentifierName("id"))))))))))),
                            ExpressionStatement(
                                InvocationExpression(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                IdentifierName("Guard"),
                                                IdentifierName("Against")),
                                            IdentifierName("EntityNotFound")))
                                    .WithArgumentList(
                                        ArgumentList(
                                            SeparatedList<ArgumentSyntax>(
                                                new SyntaxNodeOrToken[]
                                                {
                                                    Argument(
                                                        IdentifierName(_modelClassName.ToCamelCase())),
                                                    Token(SyntaxKind.CommaToken),
                                                    Argument(
                                                        InvocationExpression(
                                                                IdentifierName(
                                                                    Identifier(
                                                                        TriviaList(),
                                                                        SyntaxKind.NameOfKeyword,
                                                                        "nameof",
                                                                        "nameof",
                                                                        TriviaList())))
                                                            .WithArgumentList(
                                                                ArgumentList(
                                                                    SingletonSeparatedList(
                                                                        Argument(
                                                                            IdentifierName(_modelClassName))))))
                                                })))),
                            ReturnStatement(
                                IdentifierName(_modelClassName.ToCamelCase())))).NormalizeWhitespace();
        }

        private SyntaxNode GenerateGetAsyncMethod()
        {
            return
                MethodDeclaration(
                        GenericName(
                                Identifier("Task"))
                            .WithTypeArgumentList(
                                TypeArgumentList(
                                    SingletonSeparatedList<TypeSyntax>(
                                        GenericName(
                                                Identifier("IEnumerable"))
                                            .WithTypeArgumentList(
                                                TypeArgumentList(
                                                    SingletonSeparatedList<TypeSyntax>(
                                                        IdentifierName(_modelClassName))))))),
                        Identifier("GetAsync"))
                    .WithModifiers(
                        TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.AsyncKeyword)))
                    .WithBody(
                        Block(
                            SingletonList<StatementSyntax>(
                                ReturnStatement(
                                    AwaitExpression(
                                        InvocationExpression(
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                IdentifierName(_repositoryFieldName),
                                                IdentifierName("ListAllAsync"))))))))
                    .NormalizeWhitespace();
        }
    }
}