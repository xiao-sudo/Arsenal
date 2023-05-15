using System.Collections.Generic;
using UnityEngine;

namespace Code.Arsenal.Area
{
    [RequireComponent(typeof(Collider))]
    public class BehaviourColliderArea : BehaviourArea
    {
        private readonly HashSet<IAreaTourist> m_InAreaTourists = new();

        private void Awake()
        {
            // set area collider to trigger, prevent physical collision
            var area_collider = GetComponent<Collider>();
            area_collider.isTrigger = true;
        }

        public override bool IsTouristInArea(IAreaTourist area_tourist)
        {
            return m_InAreaTourists.Contains(area_tourist);
        }

        private void OnTriggerEnter(Collider other)
        {
            var tourist = other.GetComponent<IAreaTourist>();

            if (IsTouristCanEnter(tourist))
            {
                m_InAreaTourists.Add(tourist);
                OnTouristEnter?.Invoke(tourist);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var tourist = other.GetComponent<IAreaTourist>();

            if (IsTouristCanExit(tourist))
            {
                m_InAreaTourists.Remove(tourist);
                OnTouristExit?.Invoke(tourist);
            }
        }
    }
}