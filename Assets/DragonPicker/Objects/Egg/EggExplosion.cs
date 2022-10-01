using UnityEngine;

namespace DragonPicker
{
    public class EggExplosion : MonoBehaviour
    {
        [SerializeField] private GameObject explosionEffectPrefab;

        private void OnCollisionEnter()
        {
            var explosion = Instantiate(explosionEffectPrefab);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
