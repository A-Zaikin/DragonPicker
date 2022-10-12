using UnityEngine;

namespace DragonPicker
{
    [RequireComponent(typeof(Collider))]
    public class EggExplosion : MonoBehaviour
    {
        [SerializeField] private GameObject explosionEffectPrefab;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Energy Shield"))
                return;

            var explosion = Instantiate(explosionEffectPrefab);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
