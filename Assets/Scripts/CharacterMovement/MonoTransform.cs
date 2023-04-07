using UnityEngine;

namespace CharacterMovement
{
    public abstract class MonoTransform : MonoBehaviour
    {
        public new Transform transform;

        private void Awake()
        {
            transform = base.transform;
            PostAwake();
        }

        protected virtual void PostAwake() {}
    }
}