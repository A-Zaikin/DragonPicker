using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DragonPicker
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI scoreLabel;
        [SerializeField] private GameObject defeatScreen;
        [SerializeField] private GameObject energyShield;
        [SerializeField] private int startHealth;
        [SerializeField] private float shieldLayerRadius;
        [SerializeField] private float startNewLevelDelay;

        [Header("Prefabs")]
        [SerializeField] private GameObject energyShieldLayerPrefab;

        private int score;
        private List<GameObject> energyShieldLayers = new();

        public void IncreaseScore()
        {
            score++;
            scoreLabel.text = $"Score: {score}";
        }

        public void OnEggMissed()
        {
            var eggs = GameObject.FindGameObjectsWithTag("Dragon Egg");
            foreach (var egg in eggs)
            {
                egg.GetComponent<EggExplosion>().Explode();
            }

            if (energyShieldLayers.Count > 0)
            {
                var currentShield = energyShieldLayers[^1];
                energyShieldLayers.Remove(currentShield);
                Destroy(currentShield);
                energyShield.GetComponent<SphereCollider>().radius = energyShieldLayers.Count * shieldLayerRadius;
            }

            if (energyShieldLayers.Count == 0)
            {
                defeatScreen.SetActive(true);
                StartCoroutine(RestartLevelCoroutine());
            }
        }

        private IEnumerator RestartLevelCoroutine()
        {
            yield return new WaitForSeconds(startNewLevelDelay);
            SceneManager.LoadScene("MainScene");
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            CreateShield();
        }

        private void CreateShield()
        {
            for (var i = 0; i < startHealth; i++)
            {
                var layer = Instantiate(energyShieldLayerPrefab, energyShield.transform);
                layer.transform.localScale *= (i + 1) * shieldLayerRadius * 2;
                energyShieldLayers.Add(layer);
            }
            energyShield.GetComponent<SphereCollider>().radius = startHealth * shieldLayerRadius;
        }
    }
}
