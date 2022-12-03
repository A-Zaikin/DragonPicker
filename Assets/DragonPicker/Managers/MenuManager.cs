using TMPro;
using UnityEngine;
using YG;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI maxScoreLabel;

    private void OnEnable() => YandexGame.GetDataEvent += SdkDataReceived;

    private void OnDisable() => YandexGame.GetDataEvent -= SdkDataReceived;

    private void SdkDataReceived()
    {
        if (YandexGame.SDKEnabled && YandexGame.auth)
        {
            maxScoreLabel.text = $"Best score: {YandexGame.savesData.maxScore}";
        }
    }

    private void Start()
    {
        SdkDataReceived();
    }
}
