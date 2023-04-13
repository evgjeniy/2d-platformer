using System;

namespace Entities
{
    public abstract class RegisterBehaviour
    {
        public void RegisterAll(ref Action onUpdate, ref Action onFixedUpdate, ref Action onEnable, ref Action onDisable)
        {
            onUpdate += Update;
            onFixedUpdate += FixedUpdate;
            onEnable += OnEnable;
            onDisable += OnDisable;
        }
        
        public void UnregisterAll(ref Action onUpdate, ref Action onFixedUpdate, ref Action onEnable, ref Action onDisable)
        {
            onUpdate -= Update;
            onFixedUpdate -= FixedUpdate;
            onEnable -= OnEnable;
            onDisable -= OnDisable;
        }
        
        public void RegisterUpdate(ref Action onUpdate) => onUpdate += Update;
        public void RegisterFixedUpdate(ref Action onFixedUpdate) => onFixedUpdate += FixedUpdate;
        public void RegisterOnEnable(ref Action onEnable) => onEnable += OnEnable;
        public void RegisterOnDisable(ref Action onDisable) => onDisable += OnDisable;
        
        public void UnregisterUpdate(ref Action onUpdate) => onUpdate -= Update;
        public void UnregisterFixedUpdate(ref Action onFixedUpdate) => onFixedUpdate -= FixedUpdate;
        public void UnregisterOnEnable(ref Action onEnable) => onEnable -= OnEnable;
        public void UnregisterOnDisable(ref Action onDisable) => onDisable -= OnDisable;

        protected virtual void Update() {}
        protected virtual void FixedUpdate() {}
        protected virtual void OnEnable() {}
        protected virtual void OnDisable() {}
    }
}