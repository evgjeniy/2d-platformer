using UnityEngine;

namespace Interactable.Base
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class InteractableBehaviour<T> : MonoCashed<Collider2D> where T : IInteractable, new() 
    {
        [SerializeField] protected T interactable;
        
        protected virtual void OnTriggerEnter2D(Collider2D col) => interactable.Interact(this, col);
    }
    
    public interface IInteractable
    {
        void Interact(MonoCashed<Collider2D> sceneObject, Collider2D other);
    }

    public interface IDamageable
    {
        void TakeDamage(float damage);
    }
}