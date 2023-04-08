using UnityEngine;

namespace Overlap.Overlaps2D
{
    [System.Serializable]
    public class CircleOverlapNonAlloc2D : CircleOverlap2D
    {
        public int Size { get; private set; }

        public override void Perform() =>
            Size = Physics2D.OverlapCircleNonAlloc(OffsetPosition, radius, Colliders, searchLayer);

        protected override void DrawCollisionArea() => Gizmos.DrawWireSphere(Vector3.zero, radius);
    }
}