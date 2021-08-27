namespace Modifiers
{
    public class LegDay : AbstractEventFilterModifier<PlayerMoveSpeedFilterContext>
    {
        private const float SpeedPercent = 0.2f;
        protected override PlayerMoveSpeedFilterContext OnSuccess(PlayerMoveSpeedFilterContext e)
        {
            e.MoveSpeed += e.BaseMoveSpeed * SpeedPercent;
            e.MoveAcceleration += e.BaseMoveAcceleration * (SpeedPercent / 2);
            e.SideStrafeSpeed += e.BaseSideStrafeSpeed * SpeedPercent;
            return e;
        }

        protected override PlayerMoveSpeedFilterContext OnFailure(PlayerMoveSpeedFilterContext e)
        {
            // TODO: Set this up with different pools for different event types
            throw new System.NotImplementedException();
        }
    }
}