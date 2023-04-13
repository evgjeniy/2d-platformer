using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    [System.Serializable]
    public abstract class EntityState : RegisterBehaviour
    {
        [field: SerializeField] public float MaxHealth { get; protected set; } = 100;
        [field: SerializeField] public bool IsInvulnerable { get; protected set; }
        
        public UnityEvent<float> onHealthChanged;
        
        private float _currentHealth;

        public float CurrentHealth
        {
            get => _currentHealth;
            protected set
            {
                _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
                onHealthChanged?.Invoke(_currentHealth);
            }
        }

        public virtual void Heal(float healAmount) => CurrentHealth += healAmount;

        public virtual void TakeDamage(float damage)
        {
            if (IsInvulnerable) return;
            
            CurrentHealth -= damage;
            if (CurrentHealth == 0.0f) EntityDeath();
        }
        
        protected virtual void EntityDeath() {}
        
        public void RegisterEntity<T1, T2>(Entity<T1, T2> entity) where T1 : EntityController where T2 : EntityState => Awake(entity);
        protected virtual void Awake<T1, T2>(Entity<T1, T2> entity) where T1 : EntityController where T2 : EntityState {}
    }
}