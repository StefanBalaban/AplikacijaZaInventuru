using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generator.Generators
{
    public class SyntaxGeneratorHelper
    {
        private readonly EndpointSyntaxGenerator _endpointSyntaxGenerator = new();
        private readonly InterfaceSyntaxGenerator _interfaceSyntayGenerator = new();
        private readonly PropertiesHelper _propertiesHelper = new();
        private readonly ServicesSyntaxGenerator _servicesSyntaxGenerator = new();

        private readonly SpecificationsSyntaxGenerator _specificationsSyntaxGenerator =
            new();

        private string _modelClassName;

        public string GenerateSyntaxNode(string model)
        {
            var node = CSharpSyntaxTree.ParseText(model).GetRoot();
            var classNode = node.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
            _modelClassName = classNode.Identifier.Text;
            _propertiesHelper.ExtractPropertiesAndAttrbitues(classNode.DescendantNodes()
                .OfType<MemberDeclarationSyntax>());

            var interfaceNode = _interfaceSyntayGenerator.GenerateInterfaceNode(_modelClassName);
            var serviceClassNode =
                _servicesSyntaxGenerator.GenerateServiceClassNode(_modelClassName,
                    _propertiesHelper.PropertiesWithAttributes);
            var specificationsNode =
                _specificationsSyntaxGenerator.GenerateSpecificationsNode(_modelClassName,
                    _propertiesHelper.AttributesWithInfo);
            var endpointsNode = _endpointSyntaxGenerator.GenerateEndpointsNode(_modelClassName,
                _propertiesHelper.PropertiesWithAttributes, _propertiesHelper.PropertiesWithInfos,
                _propertiesHelper.AttributesWithInfo);

            var code = new StringBuilder();
            code.AppendLine($"// ApplicationCore\\Interfaces\\I{_modelClassName}Service.cs");
            code.AppendLine(interfaceNode.ToFullString());
            code.AppendLine($"// ApplicationCore\\Services\\{_modelClassName}Service.cs");
            code.AppendLine(serviceClassNode.ToFullString());
            code.AppendLine(
                $"// ApplicationCore\\Specifications\\{_modelClassName}\\{_modelClassName}FilterPaginatedSpecification.cs");
            code.AppendLine(
                $"// ApplicationCore\\Specifications\\{_modelClassName}\\{_modelClassName}FilterSpecification.cs");
            code.AppendLine(specificationsNode.ToFullString());
            code.AppendLine("// Infrastructure\\Data\\Context.cs");
            code.AppendLine($"public DbSet<{_modelClassName}> {_modelClassName} {{ get; set; }}");
            code.AppendLine($"// PublicApi\\Endpoints\\{_modelClassName}Endpoints\\Context.cs");
            code.AppendLine(endpointsNode.ToFullString());

            return code.ToString();
        }
    }
}