using UnityEngine;
using TMPro;
using YG;
using UnityEngine.Events;

namespace DragonPicker
{
    public class YandexSdkManager : MonoBehaviour
    {
        [SerializeField] private UnityEvent authorizationCheck;

        private bool isFirstLaunch = true;

        public void ResolvedAuthorization()
        {
            Debug.Log($"SDK available\nResolved authorization\nPlayer name: \"{YandexGame.playerName}\"");
        }

        public void RejectedAuthorization()
        {
            Debug.Log($"SDK available\nRejected authorization");
            if (!YandexGame.auth)
            {
                YandexGame.AuthDialog();
            }
        }

        private void OnEnable() => YandexGame.GetDataEvent += SdkDataReceived;

        private void OnDisable() => YandexGame.GetDataEvent -= SdkDataReceived;

        private void SdkDataReceived()
        {
            if (YandexGame.SDKEnabled && isFirstLaunch)
            {
                Debug.Log($"SDK available\nWaiting for authorization...");
                isFirstLaunch = false;
                if (!YandexGame.auth)
                {
                    authorizationCheck?.Invoke();
                }
            }
        }
    }
}
