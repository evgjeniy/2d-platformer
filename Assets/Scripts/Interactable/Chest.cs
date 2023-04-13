using Entities.Player;
using UnityEngine;

namespace Interactable
{
    [System.Serializable]
    public class Chest : IInteractable
    {
        [SerializeField] private int gemsAmount;
        
        public void Interact(MonoTransform chest, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            var keyGameObject = player.Inventory.Find<ChestKey>();
            
            if (keyGameObject == null || keyGameObject.Chest != chest) return;
            
            player.Inventory.Remove(keyGameObject);
            
            chest.gameObject.SetActive(false);
            
            // TODO - add gems to wallet
        }
    }
}