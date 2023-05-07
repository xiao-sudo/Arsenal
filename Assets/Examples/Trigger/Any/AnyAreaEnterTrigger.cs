using Code.Arsenal.Area;
using Code.Arsenal.SimplePool;
using Code.Arsenal.Trigger;

namespace Examples.Trigger.Any
{
    public sealed class AnyAreaEnterTrigger : AreaEnterTrigger
    {
        protected override void FireAreaTrigger(IAreaTourist area_tourist)
        {
            var payload = SimplePool<AnyPayload>.Acquire();
            payload.Name = "Area Enter Payload";

            TryFire(payload);
        }
    }
}