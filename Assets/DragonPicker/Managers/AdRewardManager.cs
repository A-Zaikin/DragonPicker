using UnityEngine;
using YG;

public class AdRewardManager : MonoBehaviour
{
    public void OpenAd() => YandexGame.RewVideoShow(0);

    private void OnEnable() => YandexGame.CloseVideoEvent += OnReward;

    private void OnDisable() => YandexGame.CloseVideoEvent -= OnReward;

    private void OnReward(int id) => Debug.Log("Player rewarded!");
}
