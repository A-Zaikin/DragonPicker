using UnityEngine;

namespace DragonPicker
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnergyShieldMovement : MonoBehaviour
    {
        [SerializeField] private float speedMultiplier;

        private Vector3 mousePosition;
        private new Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            mousePosition = Input.mousePosition;
        }

        private void FixedUpdate()
        {
            var distanceToCamera = transform.position.z - Camera.main.transform.position.z;
            var relativeMousePosition = mousePosition + distanceToCamera * Vector3.forward;
            var goalPosition = Camera.main.ScreenToWorldPoint(relativeMousePosition);
            rigidbody.AddForce((goalPosition.x - transform.position.x) * speedMultiplier * Vector3.right);
        }
    }
}
