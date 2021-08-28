using UnityEngine;

namespace Modifiers
{
    public class Bounces : AbstractOnFireModifier
    {
        protected override void OnSuccess(OnFireContext e)
        {
            e.Projectile.bounces += Strength;
        }
    }
}