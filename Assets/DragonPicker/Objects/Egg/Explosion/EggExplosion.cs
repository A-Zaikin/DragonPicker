using UnityEngine;
using UnityEngine.Events;

namespace DragonPicker
{
    [RequireComponent(typeof(Collider))]
    public class EggExplosion : MonoBehaviour
    {
        [SerializeField] private GameObject explosionEffectPrefab;

        public void Explode()
        {
            var explosion = Instantiate(explosionEffectPrefab);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Energy Shield"))
            {
                GameplayManager.Instance.OnEggMissed();
            }
        }
    }
}
