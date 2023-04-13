using Entities.Player;
using UnityEngine;

namespace Interactable
{
    [System.Serializable]
    public class ChestKey : IInteractable, ICollectableItem
    {
        [field: SerializeField] public MonoTransform Chest { get; set; }

        public void Interact(MonoTransform key, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            
            player.Inventory.Add(this);
            key.gameObject.SetActive(false);
        }
    }
}