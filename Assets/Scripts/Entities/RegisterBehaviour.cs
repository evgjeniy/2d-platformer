using System;

namespace Entities
{
    public abstract class RegisterBehaviour
    {
        public void RegisterAll(ref Action onStart, ref Action onUpdate, ref Action onFixedUpdate, ref Action onEnable, ref Action onDisable)
        {
            onStart += Start;
            onUpdate += Update;
            onFixedUpdate += FixedUpdate;
            onEnable += OnEnable;
            onDisable += OnDisable;
        }
        
        public void UnregisterAll(ref Action onStart, ref Action onUpdate, ref Action onFixedUpdate, ref Action onEnable, ref Action onDisable)
        {
            onStart += Start;
            onUpdate -= Update;
            onFixedUpdate -= FixedUpdate;
            onEnable -= OnEnable;
            onDisable -= OnDisable;
        }
        
        public void RegisterStart(ref Action onStart) => onStart += Start;
        public void RegisterUpdate(ref Action onUpdate) => onUpdate += Update;
        public void RegisterFixedUpdate(ref Action onFixedUpdate) => onFixedUpdate += FixedUpdate;
        public void RegisterOnEnable(ref Action onEnable) => onEnable += OnEnable;
        public void RegisterOnDisable(ref Action onDisable) => onDisable += OnDisable;
        
        public void UnregisterStart(ref Action onStart) => onStart -= Start;
        public void UnregisterUpdate(ref Action onUpdate) => onUpdate -= Update;
        public void UnregisterFixedUpdate(ref Action onFixedUpdate) => onFixedUpdate -= FixedUpdate;
        public void UnregisterOnEnable(ref Action onEnable) => onEnable -= OnEnable;
        public void UnregisterOnDisable(ref Action onDisable) => onDisable -= OnDisable;
        
        protected virtual void Start() {}
        protected virtual void Update() {}
        protected virtual void FixedUpdate() {}
        protected virtual void OnEnable() {}
        protected virtual void OnDisable() {}
    }
}