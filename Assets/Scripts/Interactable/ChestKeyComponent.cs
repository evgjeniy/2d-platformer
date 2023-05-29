using Entities.Player;
using Entities.Player.PlayerComponents;
using Interactable.Base;
using Spawners;
using UnityEngine;
using Utils;

namespace Interactable
{
    public class ChestKeyComponent : MovableInteractableBehaviour<ChestKey>
    {
        protected override void PostAwake() => GetComponent<ParticleSpawner>().IfNotNull(spawner =>
        {
            spawner.Spawn(transform)?.Play();
            spawner.DestroyComponent();
        });
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
                key.First.Disable();
                player.Inventory.Add(this);
            });
        }
    }
}