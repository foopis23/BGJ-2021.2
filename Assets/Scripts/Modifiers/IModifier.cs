namespace Modifiers
{
    public interface IModifier
    {
        public void SetCard(CardObject strength);
        public void SetStrength(int strength);
        public void Update();
        public void Activate();
        public void Deactivate();
        public string GetFlavorText();
    }
}