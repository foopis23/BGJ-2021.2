using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modifiers;

public class attackTargetDamageReduction : AbstractOnHitModifier
{
    protected override OnExpireContext OnSuccess(OnExpireContext e)
    {
        foreach (var entity in e.Projectile.AllHitEntities)
        {
            entity.ApplyStatusEffect(new StatusEffects.DamageResistance(Strength), 10.0f);
        }

        return e;
    }

    public override string GetFlavorText()
    {
        return $"Damage Resistance {Strength}: Changes how much damage an enemy will take";
    }
}
