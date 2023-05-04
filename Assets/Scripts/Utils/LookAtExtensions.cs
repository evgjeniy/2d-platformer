using UnityEngine;

namespace Utils
{
    public static class LookAtExtensions
    {
        public static void LookAt(this MonoTransform from, Transform target) => LookAt(from.transform, target);

        public static void LookAt(this MonoTransform from, Vector3 target) => LookAt(from.transform, target);

        public static void LookAt(this MonoTransform from, Vector2 target) => LookAt(from.transform, target);

        public static void LookAt(this MonoTransform from, float xDirection) => LookAt(from.transform, xDirection);
        
        public static void LookAt(this Component from, Transform target) => LookAt(from.transform, target);

        public static void LookAt(this Component from, Vector3 target) => LookAt(from.transform, target);

        public static void LookAt(this Component from, Vector2 target) => LookAt(from.transform, target);

        public static void LookAt(this Component from, float xDirection) => LookAt(from.transform, xDirection);
        
        public static void LookAt(this Transform from, Transform target) => LookAt(from, target.position.x - from.position.x);
        
        public static void LookAt(this Transform from, Vector3 target) => LookAt(from, target.x - from.position.x);

        public static void LookAt(this Transform from, Vector2 target) => LookAt(from, target.x - from.position.x);
        
        public static void LookAt(this Transform from, float xDirection)
        {
            var scale = from.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(xDirection);
            from.localScale = scale;
        }
    }
}