using UnityEngine;

namespace Overlap.Overlaps3D
{
    [System.Serializable]
    public class BoxNonAllocOverlap : BoxOverlap
    {
        public int Size { get; private set; }

        public override void Perform() => Size = Physics.OverlapBoxNonAlloc(OffsetPosition, 
            boxSize / 2.0f, Colliders, Quaternion.Euler(OffsetRotation), searchLayer);
    }
}