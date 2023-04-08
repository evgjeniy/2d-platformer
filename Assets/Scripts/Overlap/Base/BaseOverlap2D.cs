using UnityEngine;

namespace Overlap.Base
{
    public abstract class BaseOverlap2D : BaseOverlap
    {
        public new Collider2D[] Colliders { get; protected set; } = new Collider2D[64];
    }
}