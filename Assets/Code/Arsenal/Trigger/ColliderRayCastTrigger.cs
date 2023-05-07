using UnityEngine;

namespace Code.Arsenal.Trigger
{
    /// <summary>
    /// ColliderRayCastTrigger, like a marker
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ColliderRayCastTrigger : ConditionTrigger
    {
        private const int kMultipleHitLimit = 10;
        private const int kOne = 1;

        private static readonly RaycastHit[] CACHE_MULTIPLE_HITS = new RaycastHit[kMultipleHitLimit];
        private static readonly RaycastHit[] CACHE_ONE_HIT = new RaycastHit[kOne];

        /// <summary>
        /// Fire one ray casting hit ColliderRayCastTrigger
        /// </summary>
        /// <param name="ray">ray for ray casting</param>
        /// <param name="layer_mask">layer mask</param>
        /// <param name="distance">ray cast distance</param>
        /// <param name="payload">payload</param>
        public static void FireOneRayHitTrigger(Ray ray, LayerMask layer_mask, float distance, object payload)
        {
            var hit_count = Physics.RaycastNonAlloc(ray, CACHE_ONE_HIT, distance, layer_mask);
            if (hit_count > 0)
                FireOneHit(CACHE_ONE_HIT[0], payload);
        }

        /// <summary>
        /// Fire multiple ray casting hit ColliderRayCastTriggers
        /// </summary>
        /// <param name="ray">ray for ray casting</param>
        /// <param name="layer_mask">layer mask</param>
        /// <param name="distance">ray cast distance</param>
        /// <param name="payload">payload</param>
        public static void FireRayHitTriggers(Ray ray, LayerMask layer_mask, float distance, object payload)
        {
            var hit_count = Physics.RaycastNonAlloc(ray, CACHE_MULTIPLE_HITS, distance, layer_mask);

            if (hit_count > 0)
            {
                for (var i = 0; i < hit_count; ++i)
                    FireOneHit(CACHE_MULTIPLE_HITS[i], payload);
            }
        }

        /// <summary>
        /// Fire one ray casting hit ColliderRayCastTrigger
        /// </summary>
        /// <param name="hit"></param>
        /// <param name="payload"></param>
        private static void FireOneHit(RaycastHit hit, object payload)
        {
            var trigger = hit.collider.GetComponent<ColliderRayCastTrigger>();
            if (null != trigger)
                trigger.TryFire(payload);
        }
    }
}