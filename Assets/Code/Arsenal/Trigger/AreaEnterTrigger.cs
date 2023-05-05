using Code.Arsenal.Area;
using UnityEngine;

namespace Code.Arsenal.Trigger
{
    public class AreaEnterTrigger : BehaviourTrigger
    {
        [SerializeField]
        private BehaviourArea m_Area;

        public override void Init()
        {
            base.Init();

            if (null != m_Area)
                m_Area.OnTouristEnter = OnTouristEnter;
        }

        private void OnTouristEnter(IAreaTourist area_tourist)
        {
            Fire(area_tourist);
        }
    }
}