namespace Modifiers
{
    public class Pierces : AbstractOnFireModifier
    {
        protected override void OnSuccess(OnFireContext e)
        {
            e.Projectile.pierces += Strength;
        }

        public override string GetFlavorText()
        {
            return $"Pierce {Strength}: Change the amount of targets a projectile can pierce.";
        }
    }
}