using UnityEngine;

namespace Modifiers
{
    public class Ricochet : AbstractOnFireModifier
    {
        protected override void OnSuccess(OnFireContext e)
        {
            e.Projectile.bounces += 4;
        }
    }
}