using UnityEngine;

namespace Overlaps2D
{
    [System.Serializable]
    public class BoxOverlapNonAlloc2D : BoxOverlap2D
    {
        public int Size { get; private set; }
        
        public override void Perform() => 
            Size = Physics2D.OverlapBoxNonAlloc(OffsetPosition, boxSize, OffsetRotation, Colliders);
    }
}