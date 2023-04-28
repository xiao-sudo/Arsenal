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
        bool Fire(object payload);
    }

    public interface IFireableTrigger : ITrigger, IFire
    {
    }

    public interface ITriggerContainer<out T>  where T : IFireableTrigger
    {
        T[] ChildTriggers { get; }
    }

    public abstract class BehaviourTrigger : MonoBehaviour, IFireableTrigger
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

        public Action<object> FireEvent { get; set; }

        #endregion

        #region IFireEvent

        public virtual bool Fire(object payload)
        {
            if (CanFire)
            {
                FireEvent?.Invoke(payload);
                return true;
            }

            return false;
        }

        #endregion
    }
}