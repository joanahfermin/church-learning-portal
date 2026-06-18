using LearningPortal.Api.SourceGenerators.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace LearningPortal.Api.SourceGenerators.Generators;

internal static class ServiceGenerator
{
    internal static void Generate(SourceProductionContext context, INamedTypeSymbol model)
    {
        var modelName = model.Name;
        var serviceName = $"{modelName}Service";
        var interfaceName = $"I{modelName}Service";
        var dbSet = PluralizationHelper.Pluralize(modelName);

        var source = $$"""
            #nullable enable
            using LearningPortal.Api.Dtos;
            using LearningPortal.Api.Services.Interfaces;
            using LearningPortal.Data;
            using LearningPortal.Data.Model;
            using Mapster;
            using Microsoft.EntityFrameworkCore;

            namespace LearningPortal.Api.Services;

            public partial class {{serviceName}} : {{interfaceName}}
            {
                private readonly AppDbContext _context;

                public {{serviceName}}(AppDbContext context)
                {
                    _context = context;
                }

                public async Task<IEnumerable<{{modelName}}Dto>> GetAll{{modelName}}sAsync()
                {
                    var items = await _context.{{dbSet}}.ToListAsync();
                    return items.Adapt<IEnumerable<{{modelName}}Dto>>();
                }

                public async Task<{{modelName}}Dto> Get{{modelName}}ByIdAsync(long id)
                {
                    var item = await _context.{{dbSet}}.FindAsync(id)
                        ?? throw new KeyNotFoundException($"{{modelName}} with id {id} not found.");
                    return item.Adapt<{{modelName}}Dto>();
                }

                public async Task<{{modelName}}Dto> Create{{modelName}}Async(Create{{modelName}}Dto model)
                {
                    var entity = model.Adapt<{{modelName}}>();
                    _context.{{dbSet}}.Add(entity);
                    await _context.SaveChangesAsync();
                    return entity.Adapt<{{modelName}}Dto>();
                }

                public async Task<{{modelName}}Dto> Update{{modelName}}Async(long id, Update{{modelName}}Dto model)
                {
                    var entity = await _context.{{dbSet}}.FindAsync(id)
                        ?? throw new KeyNotFoundException($"{{modelName}} with id {id} not found.");

                    if (entity.Version != model.Version)
                        throw new InvalidOperationException(
                            $"{{modelName}} has been modified by another user. Please refresh and try again.");

                    model.Adapt(entity);
                    entity.Version++;

                    await _context.SaveChangesAsync();
                    return entity.Adapt<{{modelName}}Dto>();
                }

                public async Task Delete{{modelName}}Async(long id)
                {
                    var entity = await _context.{{dbSet}}.FindAsync(id)
                        ?? throw new KeyNotFoundException($"{{modelName}} with id {id} not found.");
                    _context.{{dbSet}}.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            """;

        context.AddSource($"Services\\{serviceName}.g.cs", SourceText.From(source, Encoding.UTF8));
    }
}
