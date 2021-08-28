using CallbackEvents;

namespace StatusEffects
{
    public abstract class AbstractStatusEffect
    {
        protected readonly int Level;

        protected AbstractStatusEffect(int level)
        {
            Level = level;
        }

        public abstract void StatFilter(LivingEntity e);
    }
}