using Entities.Player;
using Interactable.Base;
using Spawners;
using UnityEngine;

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
            if (player.State.CurrentHealth == player.State.MaxHealth) return; 

            PlayCollectAnimation(apple, onKill: () => Object.Destroy(apple.gameObject), onPlay: () =>
            {
                player.State.Heal(healValue);
                apple.First.enabled = false;
                apple.GetComponent<ParticleSpawner>()?.Spawn(apple.transform.position + Vector3.up)?.Play();
            });
        }
    }
}