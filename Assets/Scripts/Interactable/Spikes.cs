using Entities.Player;
using UnityEngine;

namespace Interactable
{
    [System.Serializable]
    public class Spikes : IInteractable
    {
        [SerializeField] private float knockForce = 700.0f;
        [SerializeField] private float damage = 50.0f;
        
        public void Interact(MonoTransform spikes, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            
            if (player.State.IsInvulnerable) return;
            var knockBackDirection = (player.transform.position - spikes.transform.position).normalized;
            player.Controller.Rigidbody.AddForce(knockForce * knockBackDirection);
            
            player.State.TakeDamage(damage);
        }
    }
}