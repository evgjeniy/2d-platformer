using UnityEngine;

namespace Overlaps2D
{
    [System.Serializable]
    public class CircleOverlap2D : BaseOverlap2D
    {
        [Header("Sphere Settings")]
        [SerializeField] protected float radius;

        public override void Perform() => 
            Colliders = Physics2D.OverlapCircleAll(OffsetPosition, radius, searchLayer);

        protected override void DrawCollisionArea() => Gizmos.DrawWireSphere(Vector3.zero, radius);
    }
}