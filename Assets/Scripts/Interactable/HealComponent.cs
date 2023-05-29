using Entities.Player;
using Interactable.Base;
using Spawners;
using UnityEngine;
using Utils;

namespace Interactable
{
    public class HealComponent : MovableInteractableBehaviour<Heal> {}
    
    [System.Serializable]
    public class Heal : CollectableBehaviour, IInteractable
    {
        [Header("Heal Settings")]
        [SerializeField] private float healValue;

        public void Interact(MonoCashed<Collider2D> apple, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            if (!player.State.Heal(healValue)) return; 

            PlayCollectAnimation(apple, onKill: apple.Destroy, onPlay: () =>
            {
                apple.First.Disable();
                apple.GetComponent<ParticleSpawner>()?.Spawn(apple.position + Vector3.up)?.Play();
            });
        }
    }
}