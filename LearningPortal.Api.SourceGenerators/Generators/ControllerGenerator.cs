using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace LearningPortal.Api.SourceGenerators.Generators;

internal static class ControllerGenerator
{
    internal static void Generate(SourceProductionContext context, INamedTypeSymbol model)
    {
        var modelName = model.Name;
        var controllerName = $"{modelName}Controller";
        var interfaceName = $"I{modelName}Service";
        var serviceField = $"_{char.ToLower(modelName[0])}{modelName.Substring(1)}Service";

        var source = $$"""
            #nullable enable
            using LearningPortal.Api.Dtos;
            using LearningPortal.Api.Services.Interfaces;
            using Microsoft.AspNetCore.Mvc;

            namespace LearningPortal.Api.Controllers;

            [ApiController]
            [Route("api/[controller]")]
            public partial class {{controllerName}} : ControllerBase
            {
                private readonly {{interfaceName}} {{serviceField}};

                public {{controllerName}}({{interfaceName}} {{serviceField}})
                {
                    this.{{serviceField}} = {{serviceField}};
                }

                // GET: api/{{modelName}}
                [HttpGet]
                public async Task<IActionResult> GetAll()
                {
                    return Ok(await {{serviceField}}.GetAll{{modelName}}sAsync());
                }

                // GET: api/{{modelName}}/{id}
                [HttpGet("{id}")]
                public async Task<IActionResult> GetById(long id)
                {
                    try
                    {
                        return Ok(await {{serviceField}}.Get{{modelName}}ByIdAsync(id));
                    }
                    catch (KeyNotFoundException ex)
                    {
                        return NotFound(ex.Message);
                    }
                }

                // POST: api/{{modelName}}
                [HttpPost]
                public async Task<IActionResult> Create(Create{{modelName}}Dto model)
                {
                    try
                    {
                        var result = await {{serviceField}}.Create{{modelName}}Async(model);
                        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }

                // PUT: api/{{modelName}}/{id}
                [HttpPut("{id}")]
                public async Task<IActionResult> Update(long id, Update{{modelName}}Dto model)
                {
                    try
                    {
                        return Ok(await {{serviceField}}.Update{{modelName}}Async(id, model));
                    }
                    catch (KeyNotFoundException ex)
                    {
                        return NotFound(ex.Message);
                    }
                    catch (InvalidOperationException ex)
                    {
                        return Conflict(ex.Message);
                    }
                }

                // DELETE: api/{{modelName}}/{id}
                [HttpDelete("{id}")]
                public async Task<IActionResult> Delete(long id)
                {
                    try
                    {
                        await {{serviceField}}.Delete{{modelName}}Async(id);
                        return Ok($"{{modelName}} deleted successfully.");
                    }
                    catch (KeyNotFoundException ex)
                    {
                        return NotFound(ex.Message);
                    }
                }
            }
            """;

        context.AddSource($"Controllers\\{controllerName}.g.cs", SourceText.From(source, Encoding.UTF8));
    }
}
