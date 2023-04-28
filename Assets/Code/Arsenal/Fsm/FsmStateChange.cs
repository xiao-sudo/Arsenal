using System;

namespace Code.Arsenal.Fsm
{
    public struct FsmStateChange<TState> : IDisposable where TState : class, IFsmState
    {
        private static FsmStateChange<TState> CURRENT;

        private FsmMachine<TState> m_Machine;
        private TState m_PrevState;
        private TState m_NextState;

        public static bool IsActive => null != CURRENT.m_Machine;

        public static FsmMachine<TState> Machine => CURRENT.m_Machine;

        public static TState PrevState
        {
            get
            {
#if UNITY_ASSERTIONS
                if (!IsActive)
                    throw new InvalidOperationException(
                        FsmStateExtension.GetChangeError(typeof(TState), typeof(FsmMachine<>)));
#endif

                return CURRENT.m_PrevState;
            }
        }

        public static TState NextState
        {
            get
            {
#if UNITY_ASSERTIONS
                if (!IsActive)
                    throw new InvalidOperationException(
                        FsmStateExtension.GetChangeError(typeof(TState), typeof(FsmMachine<>)));
#endif
                return CURRENT.m_NextState;
            }
        }


        /// <summary>
        /// Save previous settings in this struct and set current settings.
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="prev_state"></param>
        /// <param name="next_state"></param>
        internal FsmStateChange(FsmMachine<TState> machine, TState prev_state, TState next_state)
        {
            this = CURRENT;

            CURRENT.m_Machine = machine;
            CURRENT.m_PrevState = prev_state;
            CURRENT.m_NextState = next_state;
        }

        /// <summary>
        /// Restore previous settings.
        /// </summary>
        public void Dispose()
        {
            CURRENT = this;
        }
    }
}