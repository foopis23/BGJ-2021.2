namespace Modifiers
{
    public interface IModifier
    {
        public void SetStrength(int strength);
        public void Update();
        public void Activate();
        public void Deactivate();
        public string GetFlavorText();
    }
}