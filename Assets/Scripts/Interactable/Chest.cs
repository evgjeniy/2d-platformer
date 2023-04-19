using Entities.Player;
using UnityEngine;

namespace Interactable
{
    [System.Serializable]
    public class Chest : IInteractable
    {
        [SerializeField] private int gemsAmount;
        
        public void Interact(MonoCashed<Collider2D> chest, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            var keyGameObject = player.Inventory.Find<ChestKey>();
            
            if (keyGameObject == null || keyGameObject.Chest != chest) return;
            
            chest.First.enabled = false;
            player.Inventory.Remove(keyGameObject);
            chest.PlayCollectableAnimation(onKill: () => Object.Destroy(chest.gameObject));
            
            // TODO - add gems to wallet
        }
    }
}