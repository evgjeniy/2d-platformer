using UnityEngine;

namespace Overlaps2D
{
    [System.Serializable]
    public class BoxOverlap2D : BaseOverlap2D
    {
        [Header("Box2D Settings")]
        [SerializeField] protected Vector2 boxSize;

        public override void Perform() => 
            Colliders = Physics2D.OverlapBoxAll(OffsetPosition, boxSize, OffsetRotation);

        protected override void DrawCollisionArea() => Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}