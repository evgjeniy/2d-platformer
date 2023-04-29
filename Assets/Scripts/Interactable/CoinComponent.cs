using Entities.Player;
using Interactable.Base;
using UnityEngine;

namespace Interactable
{
    public class CoinComponent : MovableInteractableBehaviour<Coin> {}
    
    [System.Serializable]
    public class Coin : CollectableBehaviour, IInteractable
    {
        [Header("Coin Settings")]
        [SerializeField, Min(0)] private int moneyAmount;

        public void Interact(MonoCashed<Collider2D> coin, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;

            coin.First.enabled = false;
            
            PlayCollectAnimation(coin,
                onPlay: () => Debug.Log($"{player.name} collect Coin ({moneyAmount})"), // TODO - add coin in the wallet
                onKill: () => Object.Destroy(coin.gameObject));
        }
    }
}