using UnityEngine;

namespace Interactable
{
    public interface IInteractable
    {
        void Interact(MonoTransform sceneObject, Collider2D other);
    }
}