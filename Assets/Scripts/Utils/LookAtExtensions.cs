using UnityEngine;

namespace Utils
{
    public static class LookAtExtensions
    {
        public static void LookAt(this MonoCashed from, Transform target) => LookAt(from.transform, target);
        public static void LookAt(this MonoCashed from, Vector3 target) => LookAt(from.transform, target);
        public static void LookAt(this MonoCashed from, Vector2 target) => LookAt(from.transform, target);
        public static void LookAt(this MonoCashed from, float xDirection) => LookAt(from.transform, xDirection);
        
        public static void LookAt(this GameObject from, Transform target) => LookAt(from.transform, target);
        public static void LookAt(this GameObject from, Vector3 target) => LookAt(from.transform, target);
        public static void LookAt(this GameObject from, Vector2 target) => LookAt(from.transform, target);
        public static void LookAt(this GameObject from, float xDirection) => LookAt(from.transform, xDirection);
        
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