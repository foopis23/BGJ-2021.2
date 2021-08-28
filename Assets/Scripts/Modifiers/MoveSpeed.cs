namespace Modifiers
{
    public class MoveSpeed : AbstractOnPassiveModifier
    {
        private const float SpeedPercent = 0.2f;
        protected override PlayerStatusEffectContext OnSuccess(PlayerStatusEffectContext e)
        {
            e.MoveSpeed += e.BaseMoveSpeed * SpeedPercent * Strength;
            e.MoveAcceleration += e.BaseMoveAcceleration * SpeedPercent * 0.5f * Strength;
            e.SideStrafeSpeed += e.BaseSideStrafeSpeed * SpeedPercent * Strength;
            return e;
        }
    }
}