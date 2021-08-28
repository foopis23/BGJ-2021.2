namespace Modifiers
{
    public class Sluggard : AbstractEventFilterModifier<PlayerStatusEffectContext>
    {
        private const float SpeedPercent = 0.2f;
        protected override PlayerStatusEffectContext OnSuccess(PlayerStatusEffectContext e)
        {
            e.MoveSpeed -= e.BaseMoveSpeed * SpeedPercent;
            e.MoveAcceleration -= e.BaseMoveAcceleration * (SpeedPercent / 2);
            e.SideStrafeSpeed -= e.BaseSideStrafeSpeed * SpeedPercent;
            return e;
        }

        protected override PlayerStatusEffectContext OnFailure(PlayerStatusEffectContext e)
        {
            // TODO: Set this up with different pools for different event types
            throw new System.NotImplementedException();
        }
    }
}