using Interactable.Base;
using UnityEngine;

namespace Entities
{
    public interface IEntity {}
    
    public abstract class Entity<T1, T2> : MonoCashed<Collider2D>, IEntity, IDamageable where T1 : EntityController where T2 : EntityState
    {
        [field: SerializeField] public T1 Controller { get; private set; }
        [field: SerializeField] public T2 State { get; private set; }

        private event System.Action OnStartAction;
        private event System.Action OnUpdateAction;
        private event System.Action OnFixedUpdateAction;
        private event System.Action OnEnableAction;
        private event System.Action OnDisableAction;
        
        public void TakeDamage(float damage) => State.TakeDamage(damage);

        protected override void PostAwake()
        {
            Controller.RegisterEntity(this);
            Controller.RegisterAll(ref OnStartAction, ref OnUpdateAction, ref OnFixedUpdateAction, ref OnEnableAction, ref OnDisableAction); // Controller Awake
            
            State.RegisterEntity(this);
            State.RegisterAll(ref OnStartAction, ref OnUpdateAction, ref OnFixedUpdateAction, ref OnEnableAction, ref OnDisableAction); // State Awake

            OnStartAction += EntityStart;
            OnUpdateAction += EntityUpdate;
            OnFixedUpdateAction += EntityFixedUpdate;
            OnEnableAction += EntityEnable;
            OnDisableAction += EntityDisable;

            EntityAwake(); // Entity Awake
        }

        private void Start() => OnStartAction?.Invoke();
        private void Update() => OnUpdateAction?.Invoke();
        private void FixedUpdate() => OnFixedUpdateAction?.Invoke();
        private void OnEnable() => OnEnableAction?.Invoke();
        private void OnDisable() => OnDisableAction?.Invoke();

        private void OnDestroy()
        { 
            OnStartAction -= EntityStart;
            OnUpdateAction -= EntityUpdate;
            OnFixedUpdateAction -= EntityFixedUpdate;
            OnEnableAction -= EntityEnable;
            OnDisableAction -= EntityDisable;
            
            Controller.UnregisterAll(ref OnStartAction, ref OnUpdateAction, ref OnFixedUpdateAction, ref OnEnableAction, ref OnDisableAction);
            State.UnregisterAll(ref OnStartAction, ref OnUpdateAction, ref OnFixedUpdateAction, ref OnEnableAction, ref OnDisableAction);
        }

        protected virtual void EntityAwake() {}
        protected virtual void EntityStart() {}
        protected virtual void EntityUpdate() {}
        protected virtual void EntityFixedUpdate() {}
        protected virtual void EntityEnable() {}
        protected virtual void EntityDisable() {}
    }
}