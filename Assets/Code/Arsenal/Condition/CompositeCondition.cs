using UnityEngine;

namespace Code.Arsenal.Condition
{
    public class CompositeCondition : BehaviourCondition
    {
        [SerializeField]
        private BehaviourCondition[] m_Conditions;

        [SerializeField]
        private ConditionOperator m_Operator;

        public override bool Eval()
        {
            switch (m_Operator)
            {
                case ConditionOperator.And:
                    return ShortCircuitEval(false);

                case ConditionOperator.Or:
                    return ShortCircuitEval(true);

                default:
                    return false;
            }
        }

        private bool ShortCircuitEval(bool short_circuit_rs)
        {
            if (null != m_Conditions)
            {
                foreach (var condition in m_Conditions)
                {
                    if (null != condition)
                    {
                        var rs = condition.Eval();
                        if (rs == short_circuit_rs)
                            return short_circuit_rs;
                    }
                }

                return !short_circuit_rs;
            }

            return true;
        }
    }
}