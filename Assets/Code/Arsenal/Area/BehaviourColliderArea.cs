﻿using System.Collections.Generic;
using UnityEngine;

namespace Code.Arsenal.Area
{
    [RequireComponent(typeof(Collider))]
    public class BehaviourColliderArea : BehaviourArea
    {
        private readonly HashSet<IAreaTourist> m_InAreaTourists = new();

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

            if (IsTouristCanExist(tourist))
            {
                m_InAreaTourists.Remove(tourist);
                OnTouristExist?.Invoke(tourist);
            }
        }
    }
}