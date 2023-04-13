using System.Collections;
using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

namespace Entities.Player
{
    [System.Serializable]
    public class PlayerState : EntityState
    {
        [SerializeField, Min(0.0f)] private float invulnerableTime;
        [SerializeField, Min(0.0f)] private float clock;

        private PlayerEntity _playerEntity;

        private SpriteRenderer[] _spriteRenderers;

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((_playerEntity = entity as PlayerEntity) == null) return;

            _spriteRenderers = _playerEntity.GetComponentsInChildren<SpriteRenderer>();
            CurrentHealth = MaxHealth;
        }

        public override void TakeDamage(float damage)
        {
            if (IsInvulnerable) return;
            
            base.TakeDamage(damage);

            if (_spriteRenderers != null)
                _playerEntity.StartCoroutine(InvulnerabilityCoroutine());
        }

        protected override void EntityDeath()
        {
            _playerEntity.onStateChangedEvent?.Invoke(CharacterState.DeathB);
            _playerEntity.enabled = false;
        }

        private IEnumerator InvulnerabilityCoroutine()
        {
            IsInvulnerable = true;
            
            for (var i = 0.0f; i < invulnerableTime; i += clock)
            {
                foreach (var spriteRenderer in _spriteRenderers)
                {
                    var color = spriteRenderer.color;
                    color.a = 0.2f;
                    spriteRenderer.color = color;
                }

                yield return new WaitForSeconds(clock / 2);

                foreach (var spriteRenderer in _spriteRenderers)
                {
                    var color = spriteRenderer.color;
                    color.a = 1.0f;
                    spriteRenderer.color = color;
                }

                yield return new WaitForSeconds(clock / 2);
            }

            IsInvulnerable = false;
        }
    }
}