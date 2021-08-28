namespace StatusEffects
{
    public class RegenerationDelay : AbstractStatusEffect
    {
        private const float SpeedUpPercent = 0.1f;

        public RegenerationDelay(int level) : base(level) {}

        public override void StatFilter(LivingEntity e)
        {
            e.HealthRegenerationDelay -= e.BaseHealthRegenerationDelay * SpeedUpPercent * Level;
        }
    }
}