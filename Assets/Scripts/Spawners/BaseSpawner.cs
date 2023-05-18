using UnityEngine;

namespace Spawners
{
    public abstract class BaseSpawner<T> : MonoTransform where T : Component
    {
        [SerializeField] private T prefab;
        [SerializeField] private Vector3 positionOffset;
        
        public virtual T Spawn(Transform parent = null)
        {
            var instance = prefab == null ? null : Instantiate(prefab, parent);
            if (instance == null) return null;
            
            instance.transform.position += positionOffset;
            return instance;
        }
        
        public virtual T Spawn(Vector3 position)
        {
            var instance = prefab == null ? null : Instantiate(prefab, position + positionOffset, Quaternion.identity);
            return instance;
        }
    }
}