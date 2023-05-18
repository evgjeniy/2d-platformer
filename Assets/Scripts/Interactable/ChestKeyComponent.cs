using Entities.Player;
using Entities.Player.PlayerComponents;
using Interactable.Base;
using Spawners;
using UnityEngine;

namespace Interactable
{
    public class ChestKeyComponent : MovableInteractableBehaviour<ChestKey>
    {
        protected override void PostAwake()
        {
            var particleSpawner = GetComponent<ParticleSpawner>();
            if (particleSpawner == null) return;
            
            particleSpawner.Spawn(transform)?.Play();
            Destroy(particleSpawner); 
        }
    }
    
    [System.Serializable]
    public class ChestKey : CollectableBehaviour, IInteractable, ICollectableItem
    {
        [field: Header("Chest Key Settings")]
        [field: SerializeField] public MonoTransform Chest { get; private set; }

        public void Interact(MonoCashed<Collider2D> key, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            
            PlayCollectAnimation(key, onKill: () => Object.Destroy(key.gameObject), onPlay: () =>
            {
                key.First.enabled = false;
                player.Inventory.Add(this);
            });
        }
    }
}