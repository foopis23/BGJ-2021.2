namespace StatusEffects
{
    public class RegenerationSpeed : AbstractStatusEffect
    {
        private const float SpeedUpPercent = 0.1f;
        public RegenerationSpeed(int level) : base(level) {}

        public override void StatFilter(LivingEntity e)
        {
            e.HealthRegenerationTickSpeed = e.BaseHealthRegenerationTickSpeed * SpeedUpPercent * Level;
        }
    }
}