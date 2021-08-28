namespace Modifiers
{
    public class ExplosionPower : AbstractOnHitModifier
    {
        protected override OnExpireContext OnSuccess(OnExpireContext e)
        {
            e.ExplosionPower += Strength;
            return e;
        }

        public override string GetFlavorText()
        {
            return $"Explosion Power {Strength}: Increases explosion power of projectile.";
        }
    }   
}