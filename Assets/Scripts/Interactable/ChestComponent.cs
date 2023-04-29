using Entities.Player;
using Interactable.Base;
using UnityEngine;

namespace Interactable
{
    public class ChestComponent : InteractableBehaviour<Chest> {}
    
    [System.Serializable]
    public class Chest : CollectableBehaviour, IInteractable
    {
        [Header("Chest Settings")]
        [SerializeField] private int gemsAmount;
        
        public void Interact(MonoCashed<Collider2D> chest, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            
            var keyGameObject = player.Inventory.Find<ChestKey>();
            if (keyGameObject == null || keyGameObject.Chest != chest) return;
            
            PlayCollectAnimation(chest, onPlay: () => player.Inventory.Remove(keyGameObject), onKill: () =>
            {
                chest.First.enabled = false;
                
                // TODO - add gems to wallet
                Debug.Log($"Player Collect {gemsAmount} Gems!");
                
                Object.Destroy(chest.gameObject);
            });
        }
    }
}