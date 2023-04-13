using UnityEngine;

namespace Entities
{
    public abstract class Entity<T1, T2> : MonoTransform where T1 : EntityController where T2 : EntityState
    {
        [field: SerializeField] public T1 Controller { get; private set; }
        [field: SerializeField] public T2 State { get; private set; }

        private event System.Action OnUpdateAction;
        private event System.Action OnFixedUpdateAction;
        private event System.Action OnEnableAction;
        private event System.Action OnDisableAction;

        protected override void PostAwake()
        {
            Controller.RegisterEntity(this);
            Controller.RegisterAll(ref OnUpdateAction, ref OnFixedUpdateAction, 
                ref OnEnableAction, ref OnDisableAction);
            
            State.RegisterEntity(this);
            State.RegisterAll(ref OnUpdateAction, ref OnFixedUpdateAction, 
                ref OnEnableAction, ref OnDisableAction);

            OnUpdateAction += EntityUpdate;
            OnFixedUpdateAction += EntityFixedUpdate;
            OnEnableAction += EntityEnable;
            OnDisableAction += EntityDisable;
            
            EntityAwake();
        }

        private void Update() => OnUpdateAction?.Invoke();
        private void FixedUpdate() => OnFixedUpdateAction?.Invoke();
        private void OnEnable() => OnEnableAction?.Invoke();
        private void OnDisable() => OnDisableAction?.Invoke();

        protected virtual void EntityAwake() {}
        protected virtual void EntityUpdate() {}
        protected virtual void EntityFixedUpdate() {}
        protected virtual void EntityEnable() {}
        protected virtual void EntityDisable() {}
    }
}