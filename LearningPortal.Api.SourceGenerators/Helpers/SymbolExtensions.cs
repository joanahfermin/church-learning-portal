using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace LearningPortal.Api.SourceGenerators.Helpers;

internal static class SymbolExtensions
{
    private static readonly HashSet<string> IgnoreFields = new()
    {
        "Id", "Version", "CreatedAt", "CreatedById", "UpdatedAt", "UpdatedById"
    };

    internal static HashSet<string> GetIgnoreFields() => IgnoreFields;

    internal static bool IsRealEntity(INamedTypeSymbol model)
    {
        var modelNamespace = model.ContainingNamespace.ToDisplayString();
        return model.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(m => m.DeclaredAccessibility == Accessibility.Public
                     && !m.IsStatic
                     && !IgnoreFields.Contains(m.Name)
                     && IsSimpleType(m.Type, modelNamespace)
                     && !m.Name.EndsWith("Id", System.StringComparison.OrdinalIgnoreCase))
            .GroupBy(m => m.Name)
            .Any();
    }

    internal static IEnumerable<INamedTypeSymbol> GetAllClasses(
        INamespaceSymbol ns, string targetNamespace)
    {
        foreach (var type in ns.GetTypeMembers())
        {
            if (!type.IsAbstract &&
                type.TypeKind == TypeKind.Class &&
                type.ContainingNamespace.ToDisplayString() == targetNamespace)
            {
                yield return type;
            }
        }

        foreach (var childNs in ns.GetNamespaceMembers())
            foreach (var type in GetAllClasses(childNs, targetNamespace))
                yield return type;
    }

    internal static bool IsSimpleType(ITypeSymbol type, string modelNamespace)
    {
        // Unwrap nullable: string? -> string
        if (type is INamedTypeSymbol { IsGenericType: true } named &&
            named.ConstructedFrom.SpecialType == SpecialType.System_Nullable_T)
        {
            return IsSimpleType(named.TypeArguments[0], modelNamespace);
        }

        // Skip collections (ICollection<T>, IEnumerable<T>, List<T>, etc.)
        if (type is INamedTypeSymbol { IsGenericType: true } generic)
        {
            var def = generic.ConstructedFrom.ToDisplayString();
            if (def.StartsWith("System.Collections"))
                return false;

            // Skip if collection of model type
            foreach (var arg in generic.TypeArguments)
            {
                if (arg.ContainingNamespace?.ToDisplayString() == modelNamespace)
                    return false;
            }
        }

        // Skip if type is from model namespace (navigation properties)
        if (type.ContainingNamespace?.ToDisplayString() == modelNamespace)
            return false;

        return true;
    }

    internal static List<IPropertySymbol> GetOwnProperties(
        INamedTypeSymbol model, string modelNamespace)
    {
        var allProperties = new List<IPropertySymbol>();
        var current = model;

        while (current != null)
        {
            foreach (var member in current.GetMembers().OfType<IPropertySymbol>())
            {
                if (member.DeclaredAccessibility == Accessibility.Public &&
                    !member.IsStatic &&
                    !allProperties.Any(p => p.Name == member.Name))
                {
                    allProperties.Add(member);
                }
            }
            current = current.BaseType;
        }

        return allProperties
            .Where(p => !IgnoreFields.Contains(p.Name) &&
                        IsSimpleType(p.Type, modelNamespace))
            .ToList();
    }
}
