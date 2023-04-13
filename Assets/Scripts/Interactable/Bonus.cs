using System.Collections.Generic;
using Assets.HeroEditor.Common.CommonScripts;
using Entities.Player;
using UnityEngine;

namespace Interactable
{
    [System.Serializable]
    public class Bonus : IInteractable
    {
        [SerializeField] private List<string> boosters; // temp -> List<Boost>
        
        public void Interact(MonoTransform bonusGameObject, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;

            var boost = boosters.Random();
            Debug.Log(player.name + " collect Bonus" + boost);
            
            bonusGameObject.gameObject.SetActive(false);
        }
    }
}