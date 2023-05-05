﻿using UnityEngine;

namespace Code.Arsenal.Trigger
{
    public class BehaviourAnyTrigger : BehaviourTrigger, ITriggerContainer<BehaviourTrigger>
    {
        [SerializeField]
        protected BehaviourTrigger[] m_ChildTriggers;

        public override void Init()
        {
            base.Init();

            if (null != ChildTriggers)
            {
                foreach (var trigger in ChildTriggers)
                {
                    // ignore self, avoid infinite loop
                    if (null != trigger && trigger != this)
                        trigger.FireEvent = AnyFire;
                }
            }
        }

        private void AnyFire(object payload)
        {
            Fire(payload);
        }

        public BehaviourTrigger[] ChildTriggers => m_ChildTriggers;
    }
}