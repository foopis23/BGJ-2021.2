namespace Modifiers
{
    public class MoveSpeed : AbstractOnPassiveModifier
    {
        protected override void OnSuccess(OnPlayerPassiveModifierTick e)
        {
            e.MoveSpeed += e.BaseMoveSpeed * SpeedPercent * Strength;
            e.MoveAcceleration += e.BaseMoveAcceleration * SpeedPercent * 0.5f * Strength;
            e.SideStrafeSpeed += e.BaseSideStrafeSpeed * SpeedPercent * Strength;
            return e;
        }
    }
}