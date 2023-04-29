using UnityEngine;

namespace Spawners
{
    public abstract class BaseSpawner<T> : MonoTransform where T : Component
    {
        [SerializeField] private T prefab;
        
        public T Spawn(Transform parent = null)
        {
            return prefab == null ? null : Instantiate(prefab, parent);
        }

        public T Spawn(Vector3 position)
        {
            var instance = Spawn();
            if (instance == null) return null;

            instance.transform.position = position;

            return instance;
        }
    }
}