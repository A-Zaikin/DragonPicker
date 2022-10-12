using UnityEngine;

namespace DragonPicker
{
    [RequireComponent(typeof(Rigidbody))]
    public class DragonMovement : MonoBehaviour
    {
        [Range(0f, 50f)][SerializeField] private float startSpeed;
        [Range(0f, 1f)][SerializeField] private float acceleration;
        [Range(0f, 100f)][SerializeField] private float horizontalDistance;
        [Tooltip("Per physics update")]
        [Range(0f, 1f)][SerializeField] private float changeDirectionChance;

        private float currentSpeed;
        private new Rigidbody rigidbody;
        private Vector3 startingPosition;
        private Vector3 movementDirection = Vector3.right;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            startingPosition = transform.position;
            currentSpeed = startSpeed;
            rigidbody.velocity = movementDirection * currentSpeed;
        }

        private void FixedUpdate()
        {
            if (rigidbody.position.x - startingPosition.x > horizontalDistance / 2)
            {
                movementDirection = Vector3.left;
            }
            else if (rigidbody.position.x - startingPosition.x < -horizontalDistance / 2)
            {
                movementDirection = Vector3.right;
            }
            else if (Random.value < changeDirectionChance)
            {
                movementDirection *= -1;
            }
            rigidbody.velocity = movementDirection * currentSpeed;
            currentSpeed += acceleration;
        }
    }
}
