using System.Collections.Generic;
using UnityEngine;

namespace Overlaps2D
{
    [System.Serializable]
    public abstract class BaseOverlap2D
    {
        [Header("Base Settings")]
        [SerializeField] protected Transform root;
        [SerializeField] protected Vector2 positionOffset;
        [SerializeField] protected float zRotationOffset;
        [SerializeField] protected LayerMask searchLayer;

        [Header("Gizmos Settings")]
        [SerializeField] protected bool drawGizmos = true;
        [SerializeField] protected Color gizmosColor = new() { r = 1.0f, a = 0.5f };

        public LayerMask SearchLayer => searchLayer;

        protected Vector2 OffsetPosition => root == null ? positionOffset : (Vector2)root.position + new Vector2(
                positionOffset.x * Mathf.Sign(root.localScale.x), 
                positionOffset.y * Mathf.Sign(root.localScale.y));
        
        protected float OffsetRotation => root == null ? zRotationOffset : root.eulerAngles.z + zRotationOffset;
        
        public List<Collider2D> Colliders { get; protected set; } = new (64);

        public void ChangeRoot(Transform newRoot) => root = newRoot;

        public void TryDrawGizmos()
        {
            if (!drawGizmos) return;

            Gizmos.color = gizmosColor;
            Gizmos.matrix = Matrix4x4.TRS(OffsetPosition, 
                Quaternion.AngleAxis(OffsetRotation, Vector3.forward), Vector3.one);

            DrawCollisionArea();
        }

        protected virtual void DrawCollisionArea() {}
        
        public abstract void Perform();
    }
}