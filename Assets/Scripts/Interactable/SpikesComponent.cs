using Entities.Player;
using Interactable.Base;
using UnityEngine;

namespace Interactable
{
    public class SpikesComponent : InteractableBehaviour<Spikes> {}
    
    [System.Serializable]
    public class Spikes : IInteractable
    {
        [SerializeField] private float damage = 50.0f;
        
        public void Interact(MonoCashed<Collider2D> spikes, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            
            player.State.TakeDamage(damage);
        }
    }
}