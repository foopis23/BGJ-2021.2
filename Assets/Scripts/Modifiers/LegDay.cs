namespace Modifiers
{
    public class LegDay : AbstractOnPassiveModifier
    {
        private const float SpeedPercent = 0.2f;
        protected override PlayerStatusEffectContext OnSuccess(PlayerStatusEffectContext e)
        {
            e.MoveSpeed += e.BaseMoveSpeed * SpeedPercent;
            e.MoveAcceleration += e.BaseMoveAcceleration * (SpeedPercent / 2);
            e.SideStrafeSpeed += e.BaseSideStrafeSpeed * SpeedPercent;
            return e;
        }
    }
}