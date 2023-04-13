using Entities.Player;
using UnityEngine;

namespace Interactable
{
    [System.Serializable]
    public class Coin : IInteractable
    {
        [SerializeField, Min(0)] private int moneyAmount;
        
        public void Interact(MonoTransform coin, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;

            Debug.Log(player.name + " collect Coin");
            coin.gameObject.SetActive(false);
        }
    }
}