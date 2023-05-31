using Entities.Player;
using Interactable.Base;
using UnityEngine;

namespace Interactable
{
    public class SaveZoneComponent : InteractableBehaviour<DeathZone> {}

    [System.Serializable]
    public class DeathZone : IInteractable
    {
        [SerializeField] private CameraFollow levelCamera;
        
        public void Interact(MonoCashed<Collider2D> saveZone, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;

            levelCamera.RemoveTarget(player.transform);
            player.State.TakeDeathDamage();
        }
    }
}