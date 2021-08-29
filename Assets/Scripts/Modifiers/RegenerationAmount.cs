namespace Modifiers
{
    public class RegenerationAmount : AbstractOnPassiveModifier
    {
        protected override void OnSuccess(OnPlayerPassiveModifierTick e)
        {
            e.Player.ApplyStatusEffect(new StatusEffects.RegenerationAmount(Strength), e.TickTime + 1);
        }

        public override string GetFlavorText()
        {
            return $"Regeneration {Strength}: Changes the regeneration amount for the player.";
        }
    }
}