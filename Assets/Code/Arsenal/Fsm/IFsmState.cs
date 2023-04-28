using System;
using System.Diagnostics;

namespace Code.Arsenal.Fsm
{
    public interface IFsmState
    {
        /// <summary>
        /// This State can be entered
        /// </summary>
        bool CanEnterState { get; }

        /// <summary>
        /// This State can be exited
        /// </summary>
        bool CanExitState { get; }

        /// <summary>
        /// Enter the State
        /// </summary>
        void OnEnterState();

        /// <summary>
        /// Exit the State
        /// </summary>
        void OnExitState();
    }

    public interface IOwnedFsmState<TState> : IFsmState where TState : class, IFsmState
    {
        FsmMachine<TState> Owner { get; }
    }


    public static class FsmStateExtension
    {
#if UNITY_ASSERTIONS
        /// <summary>[Internal] Returns an error message explaining that the wrong type of change is being accessed.</summary>
        internal static string GetChangeError(Type state_type, Type machine_type, string change_type = "State")
        {
            Type previous_type = null;
            Type base_state_type = null;
            System.Collections.Generic.HashSet<Type> active_change_types = null;

            var text = new System.Text.StringBuilder();
            var stack_trace = new StackTrace(1, false).GetFrames();

            if (null != stack_trace)
            {
                foreach (var t in stack_trace)
                {
                    var type = t.GetMethod().DeclaringType;
                    if (type != previous_type && type is { IsGenericType: true } &&
                        type.GetGenericTypeDefinition() == machine_type)
                    {
                        var argument = type.GetGenericArguments()[0];
                        if (argument.IsAssignableFrom(state_type))
                        {
                            base_state_type = argument;
                            break;
                        }

                        active_change_types ??= new System.Collections.Generic.HashSet<Type>();
                        active_change_types.Add(argument);
                    }

                    previous_type = type;
                }

                text.Append("Attempted to access ")
                    .Append(change_type)
                    .Append("Change<")
                    .Append(state_type.FullName)
                    .Append($"> but no {nameof(FsmMachine<IFsmState>)} of that type is currently changing its ")
                    .Append(change_type)
                    .AppendLine(".");

                if (base_state_type != null)
                {
                    text.Append(" - ")
                        .Append(change_type)
                        .Append(" changes must be accessed using the base ")
                        .Append(change_type)
                        .Append(" type, which is ")
                        .Append(change_type)
                        .Append("Change<")
                        .Append(base_state_type.FullName)
                        .AppendLine("> in this case.");

                    var caller = stack_trace[1].GetMethod();
                    if (caller.DeclaringType == typeof(FsmStateExtension))
                    {
                        var property_name = stack_trace[0].GetMethod().Name;
                        property_name = property_name.Substring(4, property_name.Length - 4); // Remove the "get_".

                        text.Append(
                                " - This may be caused by the compiler incorrectly inferring the generic argument of the Get")
                            .Append(property_name)
                            .Append(" method, in which case it must be manually specified like so: state.Get")
                            .Append(property_name)
                            .Append('<')
                            .Append(base_state_type.FullName)
                            .AppendLine(">()");
                    }
                }
                else
                {
                    if (active_change_types == null)
                    {
                        text.Append(" - No other ")
                            .Append(change_type)
                            .AppendLine(" changes are currently occurring either.");
                    }
                    else
                    {
                        if (active_change_types.Count == 1)
                        {
                            text.Append(" - There is 1 ")
                                .Append(change_type)
                                .AppendLine(" change currently occurring:");
                        }
                        else
                        {
                            text.Append(" - There are ")
                                .Append(active_change_types.Count)
                                .Append(' ')
                                .Append(change_type)
                                .AppendLine(" changes currently occurring:");
                        }

                        foreach (var type in active_change_types)
                        {
                            text.Append("     - ")
                                .AppendLine(type.FullName);
                        }
                    }
                }
            }


            text.Append(" - ")
                .Append(change_type)
                .Append("Change<")
                .Append(state_type.FullName)
                .AppendLine(
                    $">.{nameof(FsmStateChange<IFsmState>.IsActive)} can be used to check if a change of that type is currently occurring.");

            return text.ToString();
        }
#endif
    }
}