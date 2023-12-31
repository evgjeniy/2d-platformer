﻿using Entities.Player;
using Interactable.Base;
using UnityEngine;

namespace Interactable
{
    public class EnemyInteractableComponent : InteractableBehaviour<EnemyInteractable> {}
    
    [System.Serializable]
    public class EnemyInteractable : IInteractable
    {
        [SerializeField] private float damage = 50.0f;
        
        public void Interact(MonoCashed<Collider2D> enemy, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            
            player.State.TakeDamage(damage);
        }
    }
}