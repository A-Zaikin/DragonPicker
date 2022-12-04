using UnityEngine;
using YG;

public class AchievementsMenu : MonoBehaviour
{
    [SerializeField] private GameObject achievementPrefab;

    private void OnEnable() => YandexGame.GetDataEvent += SdkDataReceived;

    private void OnDisable() => YandexGame.GetDataEvent -= SdkDataReceived;

    private void SdkDataReceived()
    {
        if (!YandexGame.SDKEnabled || !YandexGame.auth)
        {
            return;
        }
    }

    private void Start()
    {
        SdkDataReceived();
    }

    private void CreateUI()
    {
        foreach (var achievement in AchievementManager.Instance.CompletedAchievements)
        {
            var achievementUI = Instantiate(achievementPrefab, transform);
        }
    }
}
