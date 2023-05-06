using UnityEngine;

namespace Code.Arsenal.Condition
{
    public abstract class BehaviourCondition : MonoBehaviour, ICondition
    {
        public virtual bool Eval() => true;
    }
}