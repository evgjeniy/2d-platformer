using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.CharacterScripts;
using Buffs;
using Entities.Player.PlayerComponents;
using Spawners;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Player
{
    [System.Serializable]
    public class PlayerState : EntityState
    {
        [SerializeField] private AnimationEvents animationEvents;
        [SerializeField] private PlayerDamageAnimation damageAnimation;

        private PlayerEntity _playerEntity;
        private List<Buff> _buffs = new();
        public List<float> DamageResistanceMultipliers { get; private set; } = new();
        
        public bool IsInvulnerable { get; private set; }

        protected override void Awake<T1, T2>(Entity<T1, T2> entity)
        {
            if ((_playerEntity = entity as PlayerEntity) == null) return;

            damageAnimation.RegisterContext(_playerEntity);
        }

        protected override void Start()
        {
            CurrentHealth = MaxHealth;
            animationEvents.OnCustomEvent += OnPlayerFell;
        }

        public void AddBuff(Buff buff) => _buffs.Add(buff);

        public void RemoveBuff(Buff buff) => _buffs.Remove(buff);

        protected override void Update()
        {
            for (int i = 0; i < _buffs.Count; i++) 
                _buffs[i].UpdateTime(Time.deltaTime);
        }

        public void TakeDamageWithForce(Vector3 force, float damage)
        {
            if (IsInvulnerable) return;

            _playerEntity.Rigidbody.AddForce(force);
            TakeDamage(damage);
        }

        public override void TakeDamage(float damage)
        {
            if (IsInvulnerable) return;

            base.TakeDamage(damage / (1 + DamageResistanceMultipliers.Sum()));
            damageAnimation.Play(() => IsInvulnerable = true, () => IsInvulnerable = false);
        }

        protected override void EntityDeath()
        {
            _playerEntity.Character.SetState(CharacterState.DeathB);
            _playerEntity.Rigidbody.bodyType = RigidbodyType2D.Static;
            _playerEntity.enabled = false;
        }

        private void OnPlayerFell(string eventName)
        {
            if (eventName != "Fell") return;
            
            _playerEntity.GetComponent<ParticleSpawner>()?.Spawn(_playerEntity.transform)?.Play();
            
            animationEvents.OnCustomEvent -= OnPlayerFell;
        } 
    }
}