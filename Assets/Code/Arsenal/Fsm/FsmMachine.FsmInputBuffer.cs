using UnityEngine;

namespace Code.Arsenal.Fsm
{
    public partial class FsmMachine<TState>
    {
        public class FsmInputBuffer : FsmInputBuffer<FsmMachine<TState>>
        {
            public FsmInputBuffer() : base()
            {
            }

            public FsmInputBuffer(FsmMachine<TState> machine) : base(machine)
            {
            }
        }

        /// <summary>
        /// Buffer a state and then try to enter it in Update util timeout
        /// </summary>
        /// <typeparam name="TStateMachine"></typeparam>
        public class FsmInputBuffer<TStateMachine> where TStateMachine : FsmMachine<TState>
        {
            private TStateMachine m_Machine;

            public TStateMachine StateMachine
            {
                get => m_Machine;
                set
                {
                    m_Machine = value;
                    Clear();
                }
            }

            public TState State { get; set; }

            public float Timeout { get; set; }

            public bool IsActive => null != State;

            public FsmInputBuffer()
            {
            }

            public FsmInputBuffer(TStateMachine machine)
            {
                m_Machine = machine;
            }

            public void Buffer(TState state, float timeout)
            {
                State = state;
                Timeout = timeout;
            }

            public bool Update() => Update(Time.deltaTime);

            public bool Update(float delta_time)
            {
                if (IsActive)
                {
                    if (TryEnterState())
                    {
                        Clear();
                        return true;
                    }

                    Timeout -= delta_time;
                    if (Timeout < 0)
                        Clear();
                }

                return false;
            }

            public virtual void Clear()
            {
                State = null;
                Timeout = 0;
            }

            protected virtual bool TryEnterState() => m_Machine.TryResetState(State);
        }
    }
}