using System;
using UnityEngine;

namespace Code.Arsenal.Trigger
{
    /// <summary>
    /// Trigger interface
    /// </summary>
    public interface ITrigger
    {
        /// <summary>
        /// If the trigger can fire
        /// </summary>
        bool CanFire { get; }

        /// <summary>
        /// The actual event to be fired
        /// </summary>
        public Action<object> FireEvent { get; set; }
    }

    /// <summary>
    /// Fire interface
    /// </summary>
    public interface IFire
    {
        /// <summary>
        /// Try Fire actual event
        /// </summary>
        /// <param name="payload">event payload</param>
        /// <returns>true if the actual event can be fired</returns>
        bool TryFire(object payload);
    }

    /// <summary>
    /// A fireable trigger interface
    /// </summary>
    public interface IFireableTrigger : ITrigger, IFire
    {
    }

    /// <summary>
    /// A fireable trigger container
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITriggerContainer<out T> where T : IFireableTrigger
    {
        /// <summary>
        /// Trigger elems in this container
        /// </summary>
        T[] SubTriggers { get; }
    }

    /// <summary>
    /// Fire counter interface
    /// </summary>
    public interface IFireCounter
    {
        /// <summary>
        /// Fire count
        /// </summary>
        int FireCount { get; }
    }

    public abstract class BehaviourTrigger : MonoBehaviour, IFireableTrigger, IFireCounter
    {
        #region Unity Event Functions

        private void Awake()
        {
            Init();
        }

        #endregion

        public virtual void Init()
        {
        }

        #region ITrigger

        public virtual bool CanFire => true;

        public int FireCount { get; private set; }

        public Action<object> FireEvent { get; set; }

        #endregion

        #region IFireEvent

        public virtual bool TryFire(object payload)
        {
            if (CanFire)
            {
                PreFire(payload);

                Fire(payload);

                PostFire();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Fire event
        /// </summary>
        /// <param name="payload"></param>
        protected virtual void Fire(object payload)
        {
            ++FireCount;
            FireEvent?.Invoke(payload);
        }

        /// <summary>
        /// Callback before fire event
        /// </summary>
        /// <param name="payload"></param>
        protected virtual void PreFire(object payload)
        {
        }

        /// <summary>
        /// Callback after fire event
        /// No payload argument, because the payload may be released in FireImpl
        /// </summary>
        protected virtual void PostFire()
        {
        }

        #endregion
    }
}