using UnityEngine;

namespace Code.Arsenal.Trigger
{
    public class BehaviourAnyTrigger : ConditionTrigger, ITriggerContainer<BehaviourTrigger>
    {
        [SerializeField]
        protected BehaviourTrigger[] m_ChildTriggers;

        public override void Init()
        {
            base.Init();

            if (null != SubTriggers)
            {
                foreach (var trigger in SubTriggers)
                {
                    // ignore self, avoid infinite loop
                    if (null != trigger && trigger != this)
                        trigger.FireEvent = AnyFire;
                }
            }
        }

        private void AnyFire(object payload)
        {
            TryFire(payload);
        }

        public BehaviourTrigger[] SubTriggers => m_ChildTriggers;
    }
}