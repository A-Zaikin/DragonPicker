using UnityEngine;
using TMPro;
using YG;
using System;
using UnityEngine.Events;

namespace DragonPicker
{
    public class YandexSdkManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private UnityEvent authorizationCheck;

        private bool isFirstLaunch = true;

        public void ResolvedAuthorization()
        {
            textField.text = $"SDK available\nResolved authorization\nPlayer name: \"{YandexGame.playerName}\"";
        }

        public void RejectedAuthorization()
        {
            textField.text = $"SDK available\nRejected authorization";
        }

        private void OnEnable() => YandexGame.GetDataEvent += SdkDataReceived;

        private void OnDisable() => YandexGame.GetDataEvent -= SdkDataReceived;

        private void SdkDataReceived()
        {
            if (YandexGame.SDKEnabled && isFirstLaunch)
            {
                textField.text = $"SDK available\nWaiting for authorization...";
                authorizationCheck?.Invoke();
                isFirstLaunch = false;
            }
        }
    }
}
