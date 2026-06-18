using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace LearningPortal.Api.SourceGenerators.Generators;

internal static class ServiceInterfaceGenerator
{
    internal static void Generate(SourceProductionContext context, INamedTypeSymbol model)
    {
        var modelName = model.Name;
        var interfaceName = $"I{modelName}Service";

        var source = $$"""
            #nullable enable
            using LearningPortal.Api.Dtos;

            namespace LearningPortal.Api.Services.Interfaces;

            public partial interface {{interfaceName}}
            {
                Task<IEnumerable<{{modelName}}Dto>> GetAll{{modelName}}sAsync();
                Task<{{modelName}}Dto> Get{{modelName}}ByIdAsync(long id);
                Task<{{modelName}}Dto> Create{{modelName}}Async(Create{{modelName}}Dto model);
                Task<{{modelName}}Dto> Update{{modelName}}Async(long id, Update{{modelName}}Dto model);
                Task Delete{{modelName}}Async(long id);
            }
            """;

        context.AddSource($"Services\\Interfaces\\{interfaceName}.g.cs", SourceText.From(source, Encoding.UTF8));
    }
}
