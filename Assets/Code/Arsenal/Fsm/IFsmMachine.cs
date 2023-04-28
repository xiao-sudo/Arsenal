using System.Collections;
using UnityEngine;

namespace Code.Arsenal.Fsm
{
    public interface IFsmMachine
    {
        /// <summary>
        /// Current Active State
        /// </summary>
        object CurrentState { get; }

        /// <summary>
        /// Prev State in Changing, Get prev state when attempt to enter a new state
        /// </summary>
        object PrevStateInChanging { get; }

        /// <summary>
        /// Next State in Changing, Get next state when attempt to exit current state
        /// </summary>
        object NextStateInChanging { get; }

        /// <summary>
        /// Possible to enter the specified state
        /// </summary>
        /// <param name="state">target state</param>
        /// <returns>true if the state can be entered</returns>
        bool CanSetState(object state);

        /// <summary>
        /// Return first of the states that can currently be entered
        /// </summary>
        /// <param name="states"></param>
        /// <returns>a state possible to be entered or null</returns>
        object CanSetState(IList states);

        /// <summary>
        /// Attempt to enter the specified state
        /// </summary>
        /// <param name="state">target state</param>
        /// <returns>true if enter successful</returns>
        bool TrySetState(object state);

        /// <summary>
        /// Attempt to enter any of the specified states
        /// </summary>
        /// <param name="states"></param>
        /// <returns>true if any state can be entered</returns>
        bool TrySetState(IList states);

        /// <summary>
        /// Attempt to enter the specified state, does not check if the specified state is the current state
        /// </summary>
        /// <param name="state">target state</param>
        /// <returns></returns>
        bool TryResetState(object state);

        /// <summary>
        /// Attempt to enter any of the specified states, does not check if any of the specified states is the current state
        /// </summary>
        /// <param name="states"></param>
        /// <returns></returns>
        bool TryResetState(IList states);

        /// <summary>
        /// Force to enter the specified state
        /// </summary>
        /// <param name="state">target state</param>
        void ForceSetState(object state);

#if UNITY_ASSERTIONS
        /// <summary>
        /// Should the current state can be set to null, default is false
        /// </summary>
        bool AllowNullStates { get; }
#endif

        /// <summary>
        /// Set AllowNullStates
        /// </summary>
        /// <param name="allow"></param>
        void SetAllowNullStates(bool allow = true);

#if UNITY_EDITOR
        int GUILineCount { get; }

        void DoGUI();

        void DoGUI(ref Rect area);
#endif
    }
}