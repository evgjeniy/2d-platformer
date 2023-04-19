using UnityEngine;

namespace Interactable.Components.Base
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class InteractableBehaviour<T> : MonoCashed<Collider2D> where T : IInteractable, new() 
    {
        [SerializeField] protected T interactable;
        
        protected virtual void OnTriggerEnter2D(Collider2D col) => interactable.Interact(this, col);
    }
}