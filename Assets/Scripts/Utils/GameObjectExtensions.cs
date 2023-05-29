using System.Collections;
using UnityEngine;

namespace Utils
{
    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject, System.Action<T> setupAction = null) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null) component = gameObject.AddComponent<T>();
            setupAction?.Invoke(component);
            return component;
        }

        public static void InvokeNextFrame<T>(this T context, System.Action<T> action, float? time) where T : MonoBehaviour
        {
            context.StartCoroutine(NextFrameCoroutine());

            IEnumerator NextFrameCoroutine()
            {
                yield return time == null ? null : new WaitForSeconds(time.Value);
                action?.Invoke(context);
            }
        }

        public static T Instantiate<T>(this T prefab, Transform parent = null) where T : Object => Object.Instantiate(prefab, parent);
        public static T Instantiate<T>(this T prefab, Vector3 position, Transform parent = null) where T : Object => Object.Instantiate(prefab, position, Quaternion.identity, parent);
        public static T Instantiate<T>(this T prefab, Vector3 position, Quaternion quaternion, Transform parent = null) where T : Object => Object.Instantiate(prefab, position, quaternion, parent);

        public static void Destroy(this GameObject gameObject) => Object.Destroy(gameObject);
        public static void Destroy(this Component component) => Object.Destroy(component.gameObject);
        public static void DestroyComponent(this Component component) => Object.Destroy(component);

        public static void Destroy(this GameObject gameObject, float delay) => Object.Destroy(gameObject, delay);
        public static void Destroy(this Component component, float delay) => Object.Destroy(component.gameObject, delay);
        public static void DestroyComponent(this Component component, float delay) => Object.Destroy(component, delay);

        public static void Activate(this GameObject gameObject) => gameObject.SetActive(true);
        public static void Activate(this Component component) => component.gameObject.SetActive(true);
        
        public static void Deactivate(this GameObject gameObject) => gameObject.SetActive(false);
        public static void Deactivate(this Component component) => component.gameObject.SetActive(false);

        public static void SetParent(this GameObject target, GameObject parent) => target.transform.SetParent(parent.transform);
        public static void SetParent(this GameObject target, Component parent) => target.transform.SetParent(parent.transform);
        public static void SetParent(this Component target, GameObject parent) => target.transform.SetParent(parent.transform);
        public static void SetParent(this Component target, Component parent) => target.transform.SetParent(parent.transform);
        
        public static void CleanParent(this GameObject target) => target.transform.SetParent(null);
        public static void CleanParent(this Component target) => target.transform.SetParent(null);
        
        public static void Enable(this Behaviour component) => component.enabled = true;
        public static void Enable(this Collider component) => component.enabled = true;
        public static void Enable(this Renderer component) => component.enabled = true;
        public static void Enable(this Cloth component) => component.enabled = true;
        public static void Enable(this AnimationState component) => component.enabled = true;
        public static void Enable(this LODGroup component) => component.enabled = true;
        
        public static void Disable(this Behaviour behaviour) => behaviour.enabled = false;
        public static void Disable(this Collider component) => component.enabled = false;
        public static void Disable(this Renderer component) => component.enabled = false;
        public static void Disable(this Cloth component) => component.enabled = false;
        public static void Disable(this AnimationState component) => component.enabled = false;
        public static void Disable(this LODGroup component) => component.enabled = false;
    }
}