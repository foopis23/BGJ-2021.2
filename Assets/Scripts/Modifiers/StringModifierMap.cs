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
            { "bounces", typeof(Bounces) },
            { "bulletCount", typeof(BulletCount) },
            { "explosion", typeof(ExplosionPower) },
            { "moveSpeed", typeof(MoveSpeed) },
            { "pierces", typeof(Pierces) },
            { "spread", typeof(Spread) },
        };
    }

    public static IModifier CreateModifierFromName(string name)
    {
        if(!dict.ContainsKey(name)) return null;
        return (IModifier) Activator.CreateInstance(dict[name]);
    }
}