using UnityEngine;

namespace Overlap.Overlaps3D
{
    [System.Serializable]
    public class SphereNonAllocOverlap : SphereOverlap
    {
        public int Size { get; private set; }

        public override void Perform() => 
            Size = Physics.OverlapSphereNonAlloc(OffsetPosition, radius, Colliders, searchLayer);
    }
}