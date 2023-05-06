using UnityEngine;

namespace Code.Arsenal.Condition
{
    public abstract class BehaviourCondition : MonoBehaviour, ICondition
    {
        public virtual bool Eval() => true;
    }

    public abstract class BehaviourParamCondition<T> : MonoBehaviour, IParamCondition<T>
    {
        public bool Eval(T param) => true;
    }
}