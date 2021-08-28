using System;
using System.Collections.Generic;
using Modifiers;

public static class StringModifierMap
{
    private static Dictionary<string, Type> dict;

    static StringModifierMap()
    {
        dict = new Dictionary<string, Type>()
        {

        };
    }

    public static IModifier CreateModifierFromName(string name)
    {
        if(!dict.ContainsKey(name)) return null;
        return (IModifier) Activator.CreateInstance(dict[name]);
    }
}