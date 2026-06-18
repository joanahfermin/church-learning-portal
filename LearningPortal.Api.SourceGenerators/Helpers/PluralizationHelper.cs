namespace LearningPortal.Api.SourceGenerators.Helpers;

internal static class PluralizationHelper
{
    internal static string Pluralize(string name)
    {
        if (name.EndsWith("ey") || name.EndsWith("ay") ||
            name.EndsWith("oy") || name.EndsWith("uy"))
            return name + "s";

        if (name.EndsWith("y"))
            return name.Substring(0, name.Length - 1) + "ies";

        if (name.EndsWith("s") || name.EndsWith("x") || name.EndsWith("z") ||
            name.EndsWith("ch") || name.EndsWith("sh"))
            return name + "es";

        return name + "s";
    }
}
