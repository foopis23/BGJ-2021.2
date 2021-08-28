namespace Modifiers
{
    public class Pierces : AbstractOnFireModifier
    {
        public Pierces(int strength) : base(strength) {}

        protected override void OnSuccess(OnFireContext e)
        {
            e.Projectile.pierces += _strength;
        }
    }
}