using Code.Arsenal.Fsm;
using UnityEngine;

namespace Examples.Trigger.State.Fsm
{
    public class PlayAnimationState : FsmBehaviourState
    {
        [SerializeField]
        private string m_StateName;

        [SerializeField]
        private int m_Value;

        private Animator m_Animator;

        public void Init(Animator animator)
        {
            m_Animator = animator;
        }

        private void OnEnable()
        {
            m_Animator.SetInteger(m_StateName, m_Value);
        }
    }
}