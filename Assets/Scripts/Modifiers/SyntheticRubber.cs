namespace Modifiers
{
    public class SyntheticRubber : AbstractOnFireModifier
    {
        protected override void OnSuccess(OnFireContext e)
        {
            e.Projectile.bounces -= 3;
        }
    }
}