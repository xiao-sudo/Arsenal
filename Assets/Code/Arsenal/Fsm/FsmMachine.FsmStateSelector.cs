using System.Collections.Generic;

namespace Code.Arsenal.Fsm
{
    /// <summary>
    /// With Priority
    /// </summary>
    public interface IPrioritized : IFsmState
    {
        float Priority { get; }
    }

    public partial class FsmMachine<TState>
    {
        /// <summary>
        /// Order States by Priority, higher priority first
        /// 
        /// StateMachine.TrySetState(selector.Values) will enter the state with highest priority that can be entered
        /// </summary>
        public class FsmStateSelector : SortedList<float, TState>
        {
            public FsmStateSelector() : base(ReverseOrderComparer<float>.INSTANCE)
            {
            }

            public void Add<TPrioritized>(TPrioritized state) where TPrioritized : TState, IPrioritized
                => Add(state.Priority, state);
        }
    }

    /// <summary>
    /// Generic Reverse Order Comparer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ReverseOrderComparer<T> : IComparer<T>
    {
        public static readonly ReverseOrderComparer<T> INSTANCE = new();

        private ReverseOrderComparer()
        {
        }

        public int Compare(T x, T y)
        {
            return Comparer<T>.Default.Compare(y, x);
        }
    }
}