using Assets.HeroEditor.Common.CharacterScripts;
using UnityEngine;

namespace Entities.Player
{
    [System.Serializable]
    public class PlayerState : EntityState
    {
        [SerializeField] private PlayerDamageAnimation damageAnimation;

        private PlayerEntity _playerEntity;
        
        public bool IsInvulnerable { get; private set; }

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((_playerEntity = entity as PlayerEntity) == null) return;

            damageAnimation.RegisterContext(_playerEntity);
        }

        protected override void Start() => CurrentHealth = MaxHealth;

        public void TakeDamageWithForce(Vector3 force, float damage)
        {
            if (IsInvulnerable) return;

            base.TakeDamage(damage);
            _playerEntity.Rigidbody.AddForce(force);
            damageAnimation.Play(() => IsInvulnerable = true, () => IsInvulnerable = false);
        }

        public override void TakeDamage(float damage)
        {
            if (IsInvulnerable) return;
            
            base.TakeDamage(damage);
            damageAnimation.Play(() => IsInvulnerable = true, () => IsInvulnerable = false);
        }

        protected override void EntityDeath()
        {
            _playerEntity.Character.SetState(CharacterState.DeathB);
            _playerEntity.Rigidbody.bodyType = RigidbodyType2D.Static;
            _playerEntity.enabled = false;
        }
    }
}