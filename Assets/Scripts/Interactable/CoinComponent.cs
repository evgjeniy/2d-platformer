using Entities.Player;
using Interactable.Base;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Interactable
{
    public class CoinComponent : MovableInteractableBehaviour<Coin> {}
    
    [System.Serializable]
    public class Coin : CollectableBehaviour, IInteractable
    {
        [Header("Coin Settings")]
        [SerializeField, Min(0)] private int coinsAmount;
        [SerializeField] private UnityEvent onCoinCollect;
        
        public void Interact(MonoCashed<Collider2D> coin, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out _)) return;

            PlayCollectAnimation(coin,
                onPlay: () =>
                {
                    coin.First.Disable();
                    onCoinCollect?.Invoke();
                },
                onKill: () =>
                {
                    coin.GetComponent<MoneyCollector>()?.Collect(coinsAmount);
                    Object.Destroy(coin.gameObject);
                });
        }
    }
}