using Code.Arsenal.SimplePool;
using Code.Arsenal.Trigger;
using UnityEngine;

namespace Examples.Trigger.Any
{
    public class AnyPayload
    {
        public string Name { get; set; }
    }

    public class AnyTriggerDemo : BehaviourAnyTrigger
    {
        public override void Init()
        {
            base.Init();
            FireEvent = o =>
            {
                if (o is AnyPayload payload)
                {
                    Debug.Log($"AnyTriggerDemo: {payload.Name}");
                    SimplePool<AnyPayload>.Release(payload);
                }
            };
        }

        private void Update()
        {
            if (DemoInput.LeftMouseUp)
            {
                if (Camera.main != null)
                {
                    var payload = SimplePool<AnyPayload>.Acquire();
                    payload.Name = "Click Payload";

                    var ray = Camera.main.ScreenPointToRay(DemoInput.MousePosition);

                    ColliderRayCastTrigger.FireOneRayHitTrigger(ray, ~0, 50f, payload);
                }
            }
        }
    }
}