using Overlap.Base;
using UnityEngine;

namespace Overlap.Overlaps3D
{
    [System.Serializable]
    public class SphereOverlap : BaseOverlap
    {
        [Header("Sphere Settings")]
        [SerializeField] protected float radius;

        public override void Perform() => Colliders = Physics.OverlapSphere(OffsetPosition, radius, searchLayer);

        protected override void DrawCollisionArea() => Gizmos.DrawWireSphere(Vector3.zero, radius);
    }
}