using Code.Arsenal.Trigger;
using UnityEngine;

namespace Examples.Trigger
{
    public class ClickColliderTriggerDemo : MonoBehaviour
    {
        [SerializeField]
        private LayerMask m_Mask = ~0;

        [SerializeField]
        private float m_Distance = 50f;

        private void Update()
        {
            if (DemoInput.LeftMouseUp)
            {
                if (Camera.main != null)
                {
                    var ray = Camera.main.ScreenPointToRay(DemoInput.MousePosition);
                    ColliderRayCastTrigger.FireOneRayHitTrigger(ray, m_Mask, m_Distance, null);
                }
            }
        }
    }
}