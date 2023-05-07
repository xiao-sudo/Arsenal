using Code.Arsenal.Area;
using Code.Arsenal.Condition;
using UnityEngine;

namespace Examples.Trigger.Condition
{
    public class InAreaCondition : BehaviourCondition
    {
        [SerializeField]
        private AreaTourist m_Tourist;

        [SerializeField]
        private BehaviourArea m_Area;

        public override bool Eval()
        {
            return base.Eval() && InArea();
        }

        private bool InArea()
        {
            if (null == m_Area || null == m_Tourist)
                return false;

            return m_Area.IsTouristInArea(m_Tourist);
        }
    }
}