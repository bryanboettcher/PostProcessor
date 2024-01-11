using System;

namespace PostProcessor.Core.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Collapses an enumeration into a string array.
    /// </summary>
    public static string[] ToStringArray<TEnum>(this TEnum _) where TEnum : struct, Enum 
        => Enum.GetNames<TEnum>();
}