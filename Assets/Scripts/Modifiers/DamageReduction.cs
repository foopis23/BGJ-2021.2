using StatusEffects;

namespace Modifiers
{
    public class DamageReduction : AbstractOnPassiveModifier
    {
        protected override void OnSuccess(OnPlayerPassiveModifierTick e)
        {
            e.Player.ApplyStatusEffect(new DamageResistance(Strength), e.TickTime + 1);
        }

        public override string GetFlavorText()
        {
            return $"Damage Reduction ${Strength}: reduces incoming damage to player.";
        }
    }
}