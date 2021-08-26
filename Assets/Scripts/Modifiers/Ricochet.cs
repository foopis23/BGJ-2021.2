using UnityEngine;

namespace Modifiers
{
    public class Ricochet : AbstractModifier<OnFireContext>
    {
        protected override void OnSuccess(OnFireContext e)
        {
            Debug.Log("ACTIVATED");
        }

        protected override void OnFailure(OnFireContext e)
        {
            // TODO: Set this up with different pools for different event types
            throw new System.NotImplementedException();
        }
    }
}