using UnityEngine;

namespace Modifiers
{
    public class Bounces : AbstractOnFireModifier
    {
        protected override void OnSuccess(OnFireContext e)
        {
            e.Projectile.bounces += Strength;
        }

        public override string GetFlavorText()
        {
            return $"Bounce {Strength}: Changes the amount of wall bounces for a projectile.";
        }
    }
}