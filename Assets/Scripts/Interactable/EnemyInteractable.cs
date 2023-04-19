﻿using Entities.Player;
using UnityEngine;

namespace Interactable
{
    [System.Serializable]
    public class EnemyInteractable : IInteractable
    {
        [SerializeField] private float damage;
        [SerializeField] private float knockForce;
        
        public void Interact(MonoCashed<Collider2D> enemy, Collider2D other)
        {
            if (!other.TryGetComponent<PlayerEntity>(out var player)) return;
            
            var knockBackDirection = (player.transform.position - enemy.transform.position).normalized;
            player.State.TakeDamageWithForce(knockForce * knockBackDirection, damage);
        }
    }
}