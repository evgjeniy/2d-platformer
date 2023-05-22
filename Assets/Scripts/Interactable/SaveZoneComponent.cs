using Entities.Player;
using Interactable.Base;
using UnityEngine;

namespace Interactable
{
    public class SaveZoneComponent : InteractableBehaviour<SaveZone> {}

    [System.Serializable]
    public class SaveZone : IInteractable
    {
        [SerializeField] private CameraFollow levelCamera;
        
        public void Interact(MonoCashed<Collider2D> saveZone, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var playerEntity)) return;

            levelCamera.RemoveTarget(playerEntity.transform);
            playerEntity.TakeDamage(float.MaxValue);
        }
    }
}