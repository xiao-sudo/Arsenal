using System;
using UnityEngine;

namespace Code.Arsenal.Trigger
{
    public class ColliderEnterTrigger : BehaviourTrigger
    {
        [SerializeField]
        private Collider m_Collider;

        public Func<Collider, bool> Condition { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (null == Condition || Condition.Invoke(other))
                DoFire();
        }

        protected virtual void DoFire()
        {
            Fire(null);
        }
    }
}