using CallbackEvents;

namespace StatusEffects
{
    public abstract class AbstractStatusEffect
    {
        protected readonly int Level;

        protected AbstractStatusEffect(int level)
        {
            Level = Level;
        }

        public abstract void StatFilter(LivingEntity e);
    }
}