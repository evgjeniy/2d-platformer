using Entities.Player;
using UnityEngine;

namespace Interactable
{
    [System.Serializable]
    public class Coin : IInteractable
    {
        [SerializeField, Min(0)] private int moneyAmount;
        
        public void Interact(MonoCashed<Collider2D> coin, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;

            coin.First.enabled = false;
            
            Debug.Log( $"{player.name} collect Coin ({moneyAmount})"); // TODO - add coin in the wallet
            coin.PlayCollectableAnimation(onKill: () => Object.Destroy(coin.gameObject));
        }
    }
}