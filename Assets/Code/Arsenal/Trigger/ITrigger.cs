using System;
using UnityEngine;

namespace Code.Arsenal.Trigger
{
    public interface ITrigger
    {
        bool CanFire { get; }

        public Action<object> FireEvent { get; set; }
    }

    public interface IFire
    {
        bool TryFire(object payload);
    }

    public interface IFireableTrigger : ITrigger, IFire
    {
    }

    public interface ITriggerContainer<out T> where T : IFireableTrigger
    {
        T[] ChildTriggers { get; }
    }

    public interface IFireCounter
    {
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