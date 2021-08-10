using System.IO;
using Generator.Generators;

var code = new StreamReader("..\\ApplicationCore\\Entities\\MealAggregate\\Meal.cs").ReadToEnd();
var syntaxGeneratorHelper = new SyntaxGeneratorHelper();
var syntaxNode = syntaxGeneratorHelper.GenerateSyntaxNode(code);
await File.WriteAllTextAsync("Results.cs", syntaxNode);