namespace StatusEffects
{
    public class DamageResistance : AbstractStatusEffect
    {
        private const float DamagePerLevel = 5.0f;
        public DamageResistance(int level) : base(level) {}

        public override void StatFilter(LivingEntity e)
        {
            e.DamageResistance += DamagePerLevel * Level;
        }
    }
}