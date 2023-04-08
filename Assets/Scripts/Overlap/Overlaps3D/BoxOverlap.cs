using Overlap.Base;
using UnityEngine;

namespace Overlap.Overlaps3D
{
    [System.Serializable]
    public class BoxOverlap : BaseOverlap
    {
        [Header("Box Settings")]
        [SerializeField] protected Vector3 boxSize;

        public override void Perform() => Colliders = Physics.OverlapBox(OffsetPosition, 
            boxSize / 2.0f, Quaternion.Euler(OffsetRotation), searchLayer);

        protected override void DrawCollisionArea() => Gizmos.DrawCube(Vector3.zero, boxSize);
    }
}