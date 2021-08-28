namespace StatusEffects
{
    public class SpeedStatusEffect : AbstractStatusEffect
    {
        private const float SpeedIncreasePercent = 0.2f;
        public SpeedStatusEffect(int level) : base(level) {}

        public override void StatFilter(LivingEntity e)
        {
            e.WalkSpeed += e.BaseWalkSpeed * SpeedIncreasePercent * Level;
            e.MoveAcceleration += e.BaseMoveAcceleration * (SpeedIncreasePercent / 2) * Level;
            e.StrafeSpeed += e.BaseStrafeSpeed * SpeedIncreasePercent * Level;
        }
    }
}