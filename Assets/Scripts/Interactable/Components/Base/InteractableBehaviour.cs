using UnityEngine;

namespace Interactable.Components.Base
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class InteractableBehaviour<T1> : MonoTransform where T1 : IInteractable, new() 
    {
        [SerializeField] protected T1 interactable;
        
        protected virtual void OnTriggerEnter2D(Collider2D col) => interactable.Interact(this, col);
    }
}