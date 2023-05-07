using Code.Arsenal.Area;
using UnityEngine;

namespace Examples.Trigger
{
    public class AreaTourist : MonoBehaviour, IAreaTourist
    {
        private Transform m_Transform;

        private void Awake()
        {
            m_Transform = transform;
        }

        public Vector3 Position => m_Transform.position;

        public Quaternion Rotation => m_Transform.rotation;
    }
}