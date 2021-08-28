using StatusEffects;

namespace Modifiers
{
    public class MoveSpeed : AbstractOnPassiveModifier
    {
        private SpeedStatusEffect _effect;
        protected override void OnSuccess(OnPlayerPassiveModifierTick e)
        {
            _effect ??= new SpeedStatusEffect(Strength);
            e.Player.ApplyStatusEffect(_effect, e.TickTime + 1);
        }

        public override string GetFlavorText()
        {
            return $"Move Speed {Strength}: Change the move speed of the player";
        }
    }
}