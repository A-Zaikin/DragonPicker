using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private List<string> achievements;

    public static AchievementManager Instance;
    public List<string> CompletedAchievements { get; private set; } = new();

    private void OnEnable() => YandexGame.GetDataEvent += SdkDataReceived;

    private void OnDisable() => YandexGame.GetDataEvent -= SdkDataReceived;

    public void CompleteAchievement(int id)
    {
        if (!YandexGame.savesData.achievementIds.Contains(id))
        {
            YandexGame.savesData.achievementIds = YandexGame.savesData.achievementIds.Append(id).ToArray();
            YandexGame.SaveProgress();
        }
    }

    private void SdkDataReceived()
    {
        if (!YandexGame.SDKEnabled || !YandexGame.auth)
        {
            return;
        }

        var sortedIds = YandexGame.savesData.achievementIds.Clone() as int[];
        Array.Sort(sortedIds);
        CompletedAchievements.AddRange(sortedIds.Select(id => achievements[id]));
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SdkDataReceived();
    }
}
