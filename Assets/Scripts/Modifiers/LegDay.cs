namespace Modifiers
{
    public class LegDay : AbstractEventFilterModifier<PlayerMoveSpeedFilterContext>
    {
        private const float SpeedPercent = 0.2f;
        protected override PlayerMoveSpeedFilterContext OnSuccess(PlayerMoveSpeedFilterContext e)
        {
            e.MoveSpeed += e.BaseMoveSpeed * SpeedPercent;
            return e;
        }

        protected override PlayerMoveSpeedFilterContext OnFailure(PlayerMoveSpeedFilterContext e)
        {
            // TODO: Set this up with different pools for different event types
            throw new System.NotImplementedException();
        }
    }
}