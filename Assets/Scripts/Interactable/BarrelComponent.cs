﻿using System.Collections.Generic;
using Entities.Player;
using Interactable.Base;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Random = UnityEngine.Random;

namespace Interactable
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BarrelComponent : MonoCashed<Rigidbody2D, Collider2D>, IDamageable
    {
        [SerializeField] private UnityEvent onDamaged;
        
        private readonly List<PlayerEntity> _players = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerEntity>(out var player))
                _players.Add(player);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerEntity>(out var player))
                _players.Remove(player);
        }

        public void TakeDamage(float ignored) => _players.GetNearestPlayer(transform).IfNotNull(nearest =>
        {
            onDamaged?.Invoke();

            var xDirection = Mathf.Sign(position.x - nearest.position.x);
            First.bodyType = RigidbodyType2D.Dynamic;
            First.AddForce(new Vector2(xDirection, Random.Range(0.0f, 1.0f)) * 100.0f);

            gameObject.Destroy(1.0f);
        });
    }
}