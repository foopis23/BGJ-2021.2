namespace Modifiers
{
    public interface IModifier
    {
        public void Update();
        public void Activate();
        public void Deactivate();
    }
}