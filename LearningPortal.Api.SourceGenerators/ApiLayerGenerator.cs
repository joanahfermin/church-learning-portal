using LearningPortal.Api.SourceGenerators.Generators;
using LearningPortal.Api.SourceGenerators.Helpers;
using Microsoft.CodeAnalysis;

namespace LearningPortal.Api.SourceGenerators;

[Generator]
public class ApiLayerGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var modelNamespace = context.AnalyzerConfigOptionsProvider
            .Select(static (options, _) =>
            {
                options.GlobalOptions.TryGetValue(
                    "build_property.ApiGenerator_ModelNamespace", out var ns);
                return ns ?? string.Empty;
            });

        var models = context.CompilationProvider
            .Combine(modelNamespace)
            .SelectMany(static (pair, _) =>
            {
                var (compilation, targetNamespace) = pair;
                return SymbolExtensions.GetAllClasses(compilation.GlobalNamespace, targetNamespace);
            })
            .Where(model => SymbolExtensions.IsRealEntity(model));

        context.RegisterSourceOutput(models, DtoGenerator.Generate);
        context.RegisterSourceOutput(models, ServiceInterfaceGenerator.Generate);
        context.RegisterSourceOutput(models, ServiceGenerator.Generate);
        context.RegisterSourceOutput(models, ControllerGenerator.Generate);
    }
}
