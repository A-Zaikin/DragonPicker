using TMPro;
using UnityEngine;
using YG;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI maxScoreLabel;
    [SerializeField] private TextMeshProUGUI onlineLabel;

    private void OnEnable()
    {
        onlineLabel.text = "Offline";
        YandexGame.GetDataEvent += SdkDataReceived;
    }

    private void OnDisable() => YandexGame.GetDataEvent -= SdkDataReceived;

    private void SdkDataReceived()
    {
        if (!YandexGame.SDKEnabled || !YandexGame.auth)
        {
            return;
        }

        onlineLabel.text = "Online";
        maxScoreLabel.text = $"Best score: {YandexGame.savesData.maxScore}";
    }

    private void Start()
    {
        SdkDataReceived();
    }
}
