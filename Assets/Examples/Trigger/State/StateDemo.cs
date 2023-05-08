using Code.Arsenal.Area;
using Code.Arsenal.Fsm;
using Code.Arsenal.Trigger;
using Examples.Trigger.State.Fsm;
using UnityEngine;

namespace Examples.Trigger.State
{
    public class StateDemo : MonoBehaviour
    {
        [SerializeField]
        private BehaviourArea m_Area;

        [SerializeField]
        private PlayAnimationState m_Init;

        [SerializeField]
        private PlayAnimationState m_Warning;

        [SerializeField]
        private Animator m_Animator;

        private FsmMachine<PlayAnimationState> m_Fsm;

        [SerializeField]
        private BehaviourTrigger m_Trigger;

        private void Awake()
        {
            m_Init.Init(m_Animator);
            m_Warning.Init(m_Animator);

            m_Fsm = new FsmMachine<PlayAnimationState>();

            m_Area.OnTouristEnter += OnTouristEnter;
            m_Area.OnTouristExist += OnTouristExist;

            m_Trigger.FireEvent = o => { Debug.Log("Event Fire"); };
        }

        private void Start()
        {
            m_Fsm.TrySetState(m_Init);
        }

        private void Update()
        {
            if (m_Fsm.CurrentState == m_Warning && DemoInput.LeftMouseUp)
            {
                var ray = Camera.main.ScreenPointToRay(DemoInput.MousePosition);
                ColliderRayCastTrigger.FireOneRayHitTrigger(ray, ~0, 50f, null);
            }
        }

        private void OnTouristEnter(IAreaTourist tourist)
        {
            m_Fsm.TrySetState(m_Warning);
        }

        private void OnTouristExist(IAreaTourist tourist)
        {
            m_Fsm.TrySetState(m_Init);
        }
    }
}