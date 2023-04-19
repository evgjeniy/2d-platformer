using Entities.Player;
using UnityEngine;

namespace Interactable
{
    [System.Serializable]
    public class Spikes : IInteractable
    {
        [SerializeField] private float knockForce = 20.0f;
        [SerializeField] private float damage = 50.0f;
        
        public void Interact(MonoCashed<Collider2D> spikes, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            if (player.State.IsInvulnerable) return;
            
            var knockBackDirection = (player.transform.position - spikes.transform.position).normalized;
            player.State.TakeDamageWithForce(knockForce * knockBackDirection, damage);
        }
    }
}