using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Generator.Generators
{
    public class InterfaceSyntaxGenerator
    {
        public SyntaxNode GenerateInterfaceNode(string modelClassName)
        {
            return
                CompilationUnit()
                    .WithUsings(
                        SingletonList<UsingDirectiveSyntax>(
                            UsingDirective(
                                IdentifierName("Entities"))))
                    .WithMembers(
                        SingletonList<MemberDeclarationSyntax>(
                            NamespaceDeclaration(
                                    IdentifierName("Interfaces"))
                                .WithMembers(
                                    SingletonList<MemberDeclarationSyntax>(
                                        InterfaceDeclaration($"I{modelClassName}Service")
                                            .WithModifiers(
                                                TokenList(
                                                    Token(SyntaxKind.PublicKeyword)))
                                            .WithBaseList(
                                                BaseList(
                                                    SingletonSeparatedList<BaseTypeSyntax>(
                                                        SimpleBaseType(
                                                            GenericName(
                                                                    Identifier("ICrudServices"))
                                                                .WithTypeArgumentList(
                                                                    TypeArgumentList(
                                                                        SingletonSeparatedList<TypeSyntax>(
                                                                            IdentifierName(modelClassName))))))))))))
                    .NormalizeWhitespace();
        }
    }
}