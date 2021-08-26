namespace Modifiers
{
    public class Tnt : AbstractEventModifier<OnExpireContext>
    {
        protected override void OnSuccess(OnExpireContext e)
        {
            // TODO: tnt go boom
            throw new System.NotImplementedException();
        }

        protected override void OnFailure(OnExpireContext e)
        {
            // TODO: Set this up with different pools for different event types
            throw new System.NotImplementedException();
        }
    }
}