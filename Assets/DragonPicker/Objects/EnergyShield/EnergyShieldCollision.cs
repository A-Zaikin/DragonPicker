using UnityEngine;

namespace DragonPicker
{
    [RequireComponent(typeof(Collider))]
    public class EnergyShieldCollision : MonoBehaviour
    {
        [SerializeField] private GameObject effectPrefab;

        private void OnCollisionEnter(Collision collision)
        {
            var effect = Instantiate(effectPrefab, transform);
            effect.transform.position = collision.GetContact(0).point;

            if (collision.gameObject.CompareTag("Dragon Egg"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
