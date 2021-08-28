namespace Modifiers
{
    public class MoveSpeed : AbstractOnPassiveModifier
    {
        public MoveSpeed(int strength) : base(strength) {}

        private const float SpeedPercent = 0.2f;
        protected override PlayerStatusEffectContext OnSuccess(PlayerStatusEffectContext e)
        {
            e.MoveSpeed += e.BaseMoveSpeed * SpeedPercent * _strength;
            e.MoveAcceleration += e.BaseMoveAcceleration * SpeedPercent * 0.5f * _strength;
            e.SideStrafeSpeed += e.BaseSideStrafeSpeed * SpeedPercent * _strength;
            return e;
        }
    }
}