using UnityEngine;

namespace Overlap.Base
{
    [System.Serializable]
    public abstract class BaseOverlap
    {
        [Header("Base Settings")]
        [SerializeField] protected Transform root;
        [SerializeField] protected Vector3 positionOffset;
        [SerializeField] protected Vector3 rotationOffset;
        [SerializeField] protected LayerMask searchLayer;

        [Header("Gizmos Settings")]
        [SerializeField] protected bool drawGizmos = true;
        [SerializeField] protected Color gizmosColor = new() { r = 1.0f, a = 0.5f };

        protected Vector3 OffsetPosition => root == null ? positionOffset : root.position + positionOffset;
        protected Vector3 OffsetRotation => root == null ? rotationOffset : root.localEulerAngles + rotationOffset;
        
        public Collider[] Colliders { get; protected set; } = new Collider[64];

        public void ChangeRoot(Transform newRoot) => root = newRoot;

        public void TryDrawGizmos()
        {
            if (!drawGizmos) return;
            
            Gizmos.matrix = Matrix4x4.TRS(OffsetPosition, Quaternion.Euler(OffsetRotation), Vector3.one);
            Gizmos.color = gizmosColor;
            
            DrawCollisionArea();
        }

        protected virtual void DrawCollisionArea() {}
        
        public abstract void Perform();
    }
}