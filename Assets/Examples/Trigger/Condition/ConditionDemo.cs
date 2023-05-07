using Code.Arsenal.Trigger;
using UnityEngine;

namespace Examples.Trigger.Condition
{
    public class ConditionDemo : ConditionTrigger
    {
        public override void Init()
        {
            base.Init();

            FireEvent = o => { Debug.Log("Condition Fire"); };
        }

        private void Update()
        {
            if (DemoInput.LeftMouseUp)
                TryFire(null);
        }
    }
}