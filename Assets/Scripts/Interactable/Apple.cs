using Entities.Player;
using UnityEngine;

namespace Interactable
{
    [System.Serializable]
    public class Apple : IInteractable
    {
        [SerializeField] private float healValue;
        
        public void Interact(MonoTransform apple, Collider2D other)
        {
            other.GetComponent<PlayerEntity>()?.State.Heal(healValue);
            apple.gameObject.SetActive(false);
        }
    }
}