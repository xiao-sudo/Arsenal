using Code.Arsenal.Condition;
using UnityEngine;

namespace Code.Arsenal.Trigger
{
    public class ConditionTrigger : BehaviourTrigger
    {
        [SerializeField]
        private BehaviourCondition m_Condition;

        public override bool CanFire => null == m_Condition || m_Condition.Eval();
    }
}