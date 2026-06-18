using LearningPortal.Api.SourceGenerators.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Text;

namespace LearningPortal.Api.SourceGenerators.Generators;

internal static class DtoGenerator
{
    internal static void Generate(SourceProductionContext context, INamedTypeSymbol model)
    {
        var modelName = model.Name;
        var modelNamespace = model.ContainingNamespace.ToDisplayString();
        var ownProperties = SymbolExtensions.GetOwnProperties(model, modelNamespace);
        var propertiesSection = BuildPropertiesSection(ownProperties);

        var source = $$"""
            #nullable enable
            using System;

            namespace LearningPortal.Api.Dtos;

            public record {{modelName}}Dto
            {
                public long Id { get; init; }
                public uint Version { get; init; }
            {{propertiesSection}}
            }

            public record Create{{modelName}}Dto
            {
            {{propertiesSection}}
            }

            public record Update{{modelName}}Dto
            {
                public uint Version { get; init; }
            {{propertiesSection}}
            }
            """;

        context.AddSource($"Dtos\\{modelName}Dtos.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private static string BuildPropertiesSection(List<IPropertySymbol> properties)
    {
        var sb = new StringBuilder();
        foreach (var prop in properties)
        {
            var required = prop.Type.NullableAnnotation == NullableAnnotation.Annotated ? "" : "required ";
            sb.AppendLine($"    public {required}{prop.Type.ToDisplayString()} {prop.Name} {{ get; init; }}");
        }
        return sb.ToString();
    }
}
