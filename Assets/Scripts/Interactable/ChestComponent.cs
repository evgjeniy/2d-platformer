using Entities.Player;
using Interactable.Base;
using Interactable.InteractableAnimations;
using Spawners;
using UnityEngine;

namespace Interactable
{
    public class ChestComponent : InteractableBehaviour<Chest> {}
    
    [System.Serializable]
    public class Chest : CollectableBehaviour, IInteractable
    {
        [Header("Chest Settings")]
        [SerializeField] private int coinsAmount = 500;
        [SerializeField] private Sprite openChestSprite;
        [SerializeField] private ParticleSpawner collectParticle;
        
        public void Interact(MonoCashed<Collider2D> chest, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            
            var keyGameObject = player.Inventory.Find<ChestKey>();
            if (keyGameObject == null || keyGameObject.Chest != chest) return;
            
            chest.PlayBounceJumpAnimation(0.5f, onPlay: () =>
            {
                player.Inventory.Remove(keyGameObject);
                chest.GetComponent<MoneyCollector>()?.Collect(coinsAmount);
                chest.First.enabled = false;
                
                if (collectParticle != null) collectParticle.Spawn(chest.position)?.Play();
                if (openChestSprite != null && chest.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
                    spriteRenderer.sprite = openChestSprite;
            });
        }
    }
}