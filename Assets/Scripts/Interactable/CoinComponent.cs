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
        [SerializeField, Min(0)] private int coinsAmount;

        public void Interact(MonoCashed<Collider2D> coin, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out _)) return;

            coin.First.enabled = false;

            PlayCollectAnimation(coin, onKill: () =>
            {
                coin.GetComponent<MoneyCollector>()?.Collect(coinsAmount);
                Object.Destroy(coin.gameObject);
            });
        }
    }
}