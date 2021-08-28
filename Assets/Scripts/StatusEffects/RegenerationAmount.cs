namespace StatusEffects
{
    public class RegenerationAmount : AbstractStatusEffect
    {
        private const float HealthPerLevel = 5.0f;
        public RegenerationAmount(int level) : base(level) {}

        public override void StatFilter(LivingEntity e)
        {
            e.HealthRegeneration += HealthPerLevel * Level;
        }
    }
}