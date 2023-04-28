using System;
using System.Collections;
using System.Collections.Generic;
using Code.Arsenal.Utils;
using UnityEngine;

namespace Code.Arsenal.Fsm
{
    [Serializable]
    public partial class FsmMachine<TState> : IFsmMachine where TState : class, IFsmState
    {
#if UNITY_ASSERTIONS
        private const string kNullNotAllow = nameof(FsmMachine<TState>) + " does not allow its state to be null.";
        public bool AllowNullStates { get; private set; }
#endif

        [SerializeField]
        private TState m_CurrentState;

        public TState CurrentState => m_CurrentState;

        public TState PrevStateInChanging => FsmStateChange<TState>.PrevState;

        public TState NextStateInChanging => FsmStateChange<TState>.NextState;

        public FsmMachine()
        {
        }

        public FsmMachine(TState initial_state)
        {
#if UNITY_ASSERTIONS
            if (null == initial_state)
                throw new ArgumentNullException(nameof(initial_state), kNullNotAllow);
#endif
            using (new FsmStateChange<TState>(this, null, initial_state))
            {
                m_CurrentState = initial_state;
                initial_state.OnEnterState();
            }
        }

        public virtual void InitAfterDeserialize()
        {
            if (null != m_CurrentState)
            {
                using (new FsmStateChange<TState>(this, null, m_CurrentState))
                {
                    m_CurrentState.OnEnterState();
                }
            }
        }

        /// <summary>
        /// Is it possible to enter the specified state
        /// </summary>
        /// <param name="state">target state</param>
        /// <returns>true if possible to enter the specified state</returns>
        public bool CanSetState(TState state)
        {
#if UNITY_ASSERTIONS
#endif
            using (new FsmStateChange<TState>(this, m_CurrentState, state))
            {
                if (null != m_CurrentState && !m_CurrentState.CanExitState)
                    return false;

                if (null != state && !state.CanEnterState)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// Return the first of the states that can currently be entered
        /// </summary>
        /// <param name="states">input states</param>
        /// <returns>a state or null</returns>
        public TState CanSetState(IList<TState> states)
        {
            foreach (var state in states)
            {
                if (CanSetState(state))
                    return state;
            }

            return null;
        }

        /// <summary>
        /// Attempt to enter the specified state
        /// </summary>
        /// <param name="state">target state</param>
        /// <returns>true when enter successful</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool TrySetState(TState state)
        {
            if (state == m_CurrentState)
            {
#if UNITY_ASSERTIONS
                if (null == state && !AllowNullStates)
                    throw new ArgumentNullException(nameof(state), kNullNotAllow);
#endif

                return true;
            }

            return TryResetState(state);
        }

        /// <summary>
        /// Attempt to enter any of the specified states
        /// </summary>
        /// <param name="states">input states</param>
        /// <returns>true if enter any state successful</returns>
        public bool TrySetState(IList<TState> states)
        {
            foreach (var state in states)
            {
                if (TrySetState(state))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Attempt to enter the specified state, even if it is already the current state
        /// </summary>
        /// <param name="state">target state</param>
        /// <returns>true if enter successful</returns>
        public bool TryResetState(TState state)
        {
            if (!CanSetState(state))
                return false;

            ForceSetState(state);
            return true;
        }

        /// <summary>
        /// Attempt to enter any of the specified states, even if it is already the current state
        /// </summary>
        /// <param name="states">input state</param>
        /// <returns>true if enter any state successful</returns>
        public bool TryResetState(IList<TState> states)
        {
            foreach (var state in states)
            {
                if (TryResetState(state))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Force to enter the specified state, ignore IState.CanEnterState and IState.CanExitState
        /// </summary>
        /// <param name="state">target state</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void ForceSetState(TState state)
        {
#if UNITY_ASSERTIONS
            if (null == state)
            {
                if (!AllowNullStates)
                    throw new ArgumentNullException(nameof(state), kNullNotAllow);
            }
            else if (state is IOwnedFsmState<TState> owned_fsm_state && owned_fsm_state.Owner != this)
            {
                throw new InvalidOperationException(
                    $"Attempt to use a state that is owned by another state machine. \n* State: {state} \n* Machine: {this}");
            }
#endif
            using (new FsmStateChange<TState>(this, m_CurrentState, state))
            {
                m_CurrentState?.OnExitState();
                m_CurrentState = state;
                m_CurrentState?.OnEnterState();
            }
        }

        [System.Diagnostics.Conditional("UNITY_ASSERTIONS")]
        public void SetAllowNullStates(bool allow = true)
        {
#if UNITY_ASSERTIONS
            AllowNullStates = allow;
#endif
        }

        #region IFsmMachine

        object IFsmMachine.CurrentState => CurrentState;
        object IFsmMachine.PrevStateInChanging => PrevStateInChanging;
        object IFsmMachine.NextStateInChanging => NextStateInChanging;

        bool IFsmMachine.CanSetState(object state) => CanSetState((TState)state);

        object IFsmMachine.CanSetState(IList states) => CanSetState((List<TState>)states);

        bool IFsmMachine.TrySetState(object state) => TrySetState((TState)state);

        bool IFsmMachine.TrySetState(IList states) => TrySetState((List<TState>)states);

        bool IFsmMachine.TryResetState(object state) => TryResetState((TState)state);

        bool IFsmMachine.TryResetState(IList states) => TryResetState((List<TState>)states);

        void IFsmMachine.ForceSetState(object state) => ForceSetState((TState)state);

        void IFsmMachine.SetAllowNullStates(bool allow) => SetAllowNullStates(allow);

        #endregion

#if UNITY_EDITOR
        public int GUILineCount => 1;

        public void DoGUI()
        {
            var spacing = UnityEditor.EditorGUIUtility.standardVerticalSpacing;
            var lines = GUILineCount;

            var height = UnityEditor.EditorGUIUtility.singleLineHeight * lines + spacing * (lines - 1);
            var area = GUILayoutUtility.GetRect(0, height);
            area.height -= spacing;

            DoGUI(ref area);
        }

        public void DoGUI(ref Rect area)
        {
            area.height = UnityEditor.EditorGUIUtility.singleLineHeight;

            using (var check = new UnityEditor.EditorGUI.ChangeCheckScope())
            {
                var state = EditorUtilities.DoGenericField(area, "Current State", m_CurrentState);

                if (check.changed)
                {
                    if (Event.current.control)
                        ForceSetState(state);
                    else
                        TrySetState(state);
                }
            }

            EditorUtilities.NextVerticalArea(ref area);
        }
#endif
    }
}