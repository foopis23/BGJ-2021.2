using UnityEngine;

namespace Modifiers
{
    public class Bounces : AbstractOnFireModifier
    {
        public Bounces(int strength) : base(strength) {}

        protected override void OnSuccess(OnFireContext e)
        {
            e.Projectile.bounces += _strength;
        }
    }
}