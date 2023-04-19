using Entities.Player;
using UnityEngine;

namespace Interactable
{
    [System.Serializable]
    public class ChestKey : IInteractable, ICollectableItem
    {
        [field: SerializeField] public MonoTransform Chest { get; set; }

        public void Interact(MonoCashed<Collider2D> key, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;

            key.First.enabled = false;
            
            player.Inventory.Add(this);
            key.PlayCollectableAnimation(onKill: () => Object.Destroy(key.gameObject));
        }
    }
}