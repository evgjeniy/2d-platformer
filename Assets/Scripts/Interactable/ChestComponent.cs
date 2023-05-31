using Entities.Player;
using Interactable.Base;
using Interactable.InteractableAnimations;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Interactable
{
    public class ChestComponent : InteractableBehaviour<Chest> {}
    
    [System.Serializable]
    public class Chest : CollectableBehaviour, IInteractable
    {
        [Header("Chest Settings")]
        [SerializeField] private UnityEvent onChestCollected;

        public void Interact(MonoCashed<Collider2D> chest, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            
            var keyGameObject = player.Inventory.Find<ChestKey>();
            if (keyGameObject == null || keyGameObject.Chest != chest) return;
            
            chest.PlayBounceJumpAnimation(0.5f, onPlay: () =>
            {
                player.Inventory.Remove(keyGameObject);
                chest.First.Disable();
                
                onChestCollected?.Invoke();
            });
        }
    }
}