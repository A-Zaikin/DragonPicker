using UnityEngine;
using UnityEngine.UI;

public class VolumeLoader : MonoBehaviour
{
    [SerializeField] private string settingsName;
    [SerializeField] private Slider menuSlider;

    private AudioSource audioSource;

    public void SaveVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(settingsName, volume);
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey(settingsName))
        {
            return;
        }

        var volume = PlayerPrefs.GetFloat(settingsName);
        audioSource.volume = volume;
        if (menuSlider != null)
        {
            menuSlider.value = volume;
        }
    }
}
