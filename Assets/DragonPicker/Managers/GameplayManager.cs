using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace DragonPicker
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI scoreLabel;
        [SerializeField] private TextMeshProUGUI playerNameLabel;
        [SerializeField] private GameObject defeatScreen;
        [SerializeField] private GameObject mage;
        [SerializeField] private GameObject mageLight;
        [SerializeField] private GameObject energyShield;
        [SerializeField] private int startHealth;
        [SerializeField] private float shieldLayerRadius;
        [SerializeField] private float startNewLevelDelay;

        [Header("Prefabs")]
        [SerializeField] private GameObject energyShieldLayerPrefab;

        private int score;
        private List<GameObject> energyShieldLayers = new();
        private AudioSource eggMissedAudioEffect;

        public void IncreaseScore()
        {
            score++;
            if (score >= 1)
            {
                AchievementManager.Instance.CompleteAchievement(1);
            }
            if (score >= 10)
            {
                AchievementManager.Instance.CompleteAchievement(2);
            }
            if (score >= 20)
            {
                AchievementManager.Instance.CompleteAchievement(3);
            }
            if (score > YandexGame.savesData.maxScore)
            {
                YandexGame.savesData.maxScore = score;
                YandexGame.SaveProgress();
                YandexGame.NewLeaderboardScores("TopPlayers", score);
            }
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
                energyShield.SetActive(false);
                mage.GetComponent<Animator>().SetBool("IsAlive", false);
                mageLight.SetActive(false);
                YandexGame.RewVideoShow(0);
                AchievementManager.Instance.CompleteAchievement(0);
                StartCoroutine(RestartLevelCoroutine());
            }
            eggMissedAudioEffect.Play();
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
            eggMissedAudioEffect = GetComponent<AudioSource>();
        }

        private void Start()
        {
            CreateShield();
            SdkDataReceived();
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

        private void OnEnable() => YandexGame.GetDataEvent += SdkDataReceived;

        private void OnDisable() => YandexGame.GetDataEvent -= SdkDataReceived;

        private void SdkDataReceived()
        {
            if (YandexGame.SDKEnabled && YandexGame.auth)
            {
                playerNameLabel.text = YandexGame.playerName;
            }
        }
    }
}
