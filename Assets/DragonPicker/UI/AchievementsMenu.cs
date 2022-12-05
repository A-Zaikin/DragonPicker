using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
        CreateUI();
    }

    private void Start()
    {
        SdkDataReceived();
    }

    private void CreateUI()
    {
        var contentContainer = transform.Find("ScrollView/Viewport/HorizontalLayout");
        foreach (var achievement in AchievementManager.Instance.CompletedAchievements)
        {
            var achievementUI = Instantiate(achievementPrefab, contentContainer);
            achievementUI.transform.Find("Inner/Image").GetComponent<Image>().sprite = achievement.Image;
            achievementUI.transform.Find("Inner/Title").GetComponent<TextMeshProUGUI>().text = achievement.Title;
            achievementUI.transform.Find("Inner/Description").GetComponent<TextMeshProUGUI>().text = achievement.Description;
        }

        // test achievements
        for (var i = 0; i < 5; i++)
        {
            Instantiate(achievementPrefab, contentContainer);
        }
    }
}
