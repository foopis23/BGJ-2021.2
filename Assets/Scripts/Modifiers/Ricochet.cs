using UnityEngine;

namespace Modifiers
{
    public class Ricochet : AbstractEventModifier<OnFireContext>
    {
        protected override void OnSuccess(OnFireContext e)
        {
            e.Projectile.bounces += 4;
        }

        protected override void OnFailure(OnFireContext e)
        {
            // TODO: Set this up with different pools for different event types
            throw new System.NotImplementedException();
        }
    }
}