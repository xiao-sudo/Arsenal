using System.Collections.Generic;
using Code.Arsenal.SimplePool;
using UnityEngine;

namespace Code.Arsenal.Fsm
{
    public partial class FsmMachine<TState>
    {
        public class FsmInputBufferSequence<TStateMachine> where TStateMachine : FsmMachine<TState>
        {
            protected class SequenceUnit
            {
                public TState State { get; set; }
                public float Timeout { get; set; }

                public void Release()
                {
                    State = null;
                    Timeout = 0;
                }
            }

            private readonly LinkedList<SequenceUnit> m_SequenceUnits = new();

            private TStateMachine m_Machine;

            public TStateMachine Machine
            {
                get => m_Machine;
                set
                {
                    m_Machine = value;
                    Clear();
                }
            }

            public bool IsActive => m_SequenceUnits.Count > 0;

            public FsmInputBufferSequence(TStateMachine machine)
            {
                m_Machine = machine;
            }

            public void Buffer(TState state, float timeout)
            {
                var unit = SimplePool<SequenceUnit>.Acquire();
                unit.State = state;
                unit.Timeout = timeout;
                m_SequenceUnits.AddLast(unit);
            }

            public bool Update() => Update(Time.deltaTime);

            public bool Update(float delta_time)
            {
                if (IsActive)
                {
                    var node = m_SequenceUnits.First;
                    var unit = node.Value;

                    // enter state successful
                    if (TryUnit(unit))
                    {
                        ReleaseUnit(unit);
                        m_SequenceUnits.RemoveFirst();
                        return true;
                    }

                    // Update all time out
                    foreach (var sequence_unit in m_SequenceUnits)
                        sequence_unit.Timeout -= delta_time;

                    // remove timeout units
                    while (m_SequenceUnits.Count > 0)
                    {
                        var head = m_SequenceUnits.First;
                        if (head.Value.Timeout < 0)
                            m_SequenceUnits.RemoveFirst();
                        else
                            break;
                    }

                    return false;
                }

                return false;
            }

            public void Clear()
            {
                m_SequenceUnits.Clear();
            }

            protected virtual bool TryUnit(SequenceUnit unit)
            {
                return m_Machine.TrySetState(unit.State);
            }

            private void ReleaseUnit(SequenceUnit unit)
            {
                unit.Release();
                SimplePool<SequenceUnit>.Release(unit);
            }
        }
    }
}