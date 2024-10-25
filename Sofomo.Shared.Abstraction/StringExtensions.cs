namespace Sofomo.Shared.Abstraction;

public static class StringExtensions
{
    public static string ToSnakeCase(this string value)
   => string.Concat((value ?? string.Empty).Select((x, i) => i > 0 && char.IsUpper(x) 
   && !char.IsUpper(value[i - 1]) ? $"_{x}" : x.ToString())).ToLower();
}
