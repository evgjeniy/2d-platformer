using System;
using DG.Tweening;
using UnityEngine;

namespace Interactable
{
    public interface IInteractable
    {
        void Interact(MonoCashed<Collider2D> sceneObject, Collider2D other);
    }

    public static class InteractableExtensions
    {
        public static void PlayCollectableAnimation(this MonoTransform collectable, Action onKill = null)
        {
            collectable.GetComponent<SpriteRenderer>()?
                .DOFade(0.0f, 0.3f).SetEase(Ease.InExpo).SetLink(collectable.gameObject);
            
            collectable.transform
                .DOMoveY(collectable.transform.position.y + 1.5f, 0.5f).SetEase(Ease.OutExpo)
                .SetLink(collectable.gameObject).OnKill(() => onKill?.Invoke());
        }
    }
}