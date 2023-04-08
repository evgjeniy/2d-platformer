using System.Linq;
using UnityEngine;

namespace Movement.SurfaceMovement
{
    public class SurfaceSlider : MonoBehaviour
    {
        private Vector2 _normal;
        
        public bool IsGrounded { get; private set; }

        public Vector2 Project(Vector2 forward) => forward - Vector3.Dot(forward, _normal) * _normal;

        private void OnCollisionEnter2D(Collision2D col)
        {
            foreach (var contactPoint2D in col.contacts)
            {
                if (!contactPoint2D.collider.CompareTag("Ground")) continue;
                
                _normal = contactPoint2D.normal;
                IsGrounded = true;
                break;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (!other.contacts.Any(point => point.collider.CompareTag("Ground"))) return;
            
            _normal = Vector2.up;
            IsGrounded = false;
        }
    }
}