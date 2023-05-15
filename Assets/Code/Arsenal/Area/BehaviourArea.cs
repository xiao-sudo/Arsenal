using System;
using UnityEngine;

namespace Code.Arsenal.Area
{
    public abstract class BehaviourArea : MonoBehaviour, IAreaTouristSensor
    {
        public Action<IAreaTourist> OnTouristEnter { get; set; }
        
        public Action<IAreaTourist> OnTouristExit { get; set; }
        
        public abstract bool IsTouristInArea(IAreaTourist area_tourist);

        public virtual bool IsTouristCanEnter(IAreaTourist area_tourist)
        {
            return null != area_tourist;
        }

        public virtual bool IsTouristCanExit(IAreaTourist area_tourist)
        {
            return null != area_tourist;
        }
    }
}