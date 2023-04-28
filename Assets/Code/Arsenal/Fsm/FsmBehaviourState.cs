using UnityEngine;

namespace Code.Arsenal.Fsm
{
    public abstract class FsmBehaviourState : MonoBehaviour, IFsmState
    {
        public virtual bool CanEnterState => true;
        public virtual bool CanExitState => true;

        public virtual void OnEnterState()
        {
#if UNITY_ASSERTIONS
            if (enabled)
                Debug.LogError($"{nameof(FsmBehaviourState)} was already enabled before {nameof(OnEnterState)} : {this}");
#endif

#if UNITY_EDITOR
            else
                UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
#endif

            enabled = true;
        }

        public virtual void OnExitState()
        {
            if (this == null)
                return;

#if UNITY_ASSERTIONS
            if (!enabled)
                Debug.LogError($"{nameof(FsmBehaviourState)} was already disabled before {nameof(OnExitState)} : {this}");
#endif

            enabled = false;
        }


#if UNITY_EDITOR
        protected void OnValidate()
        {
            if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                return;

            enabled = false;
        }
#endif
    }
}