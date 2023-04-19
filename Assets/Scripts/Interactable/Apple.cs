using Entities.Player;
using UnityEngine;

namespace Interactable
{
    [System.Serializable]
    public class Apple : IInteractable
    {
        [SerializeField] private float healValue;
        
        public void Interact(MonoCashed<Collider2D> apple, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            
            player.State.Heal(healValue);
            apple.First.enabled = false;
            apple.PlayCollectableAnimation(onKill: () => Object.Destroy(apple.gameObject));
        }
    }
}