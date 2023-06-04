using UnityEngine;
using Utils;

namespace Spawners
{
    public abstract class BaseSpawner<T> : MonoCashed where T : Component
    {
        [SerializeField] private T prefab;
        [SerializeField] private Vector3 positionOffset;

        public virtual T Spawn(Transform parent = null) => prefab.IfNotNull(() =>
        {
            var newInstance = Instantiate(prefab, parent);
            newInstance.transform.position += positionOffset;
            return newInstance;
        });

        public virtual T Spawn(Vector3 spawnPosition) => prefab.IfNotNull(() => 
            Instantiate(prefab, spawnPosition + positionOffset, Quaternion.identity));
        
        public void Spawn() => prefab.IfNotNull(() => Instantiate(prefab).transform.position += positionOffset);
    }
}