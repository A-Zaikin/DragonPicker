using System.Collections;
using UnityEngine;

namespace DragonPicker
{
    public class EggDropper : MonoBehaviour
    {
        [Range(0f, 30f)][SerializeField] private float timeBetweenEggDrops;
        [Range(-10f, 10f)][SerializeField] private float spawnVerticalDistance;

        [Header("Prefabs")]
        [SerializeField] private GameObject eggPrefab;

        private void Start()
        {
            StartCoroutine(EggDroppingCoroutine());
        }

        private IEnumerator EggDroppingCoroutine()
        {
            yield return new WaitForSeconds(2);
            while (true)
            {
                var egg = Instantiate(eggPrefab);
                egg.transform.position = transform.position + Vector3.down * spawnVerticalDistance;
                yield return new WaitForSeconds(timeBetweenEggDrops);
            }
        }
    }
}
