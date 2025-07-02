using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambienceSlider;

    private void Start()
    {
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
        SetAmbienceVolume();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);     // Wir lieben Mathe
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void SetAmbienceVolume()
    {
        float volume = ambienceSlider.value;
        audioMixer.SetFloat("Ambience", Mathf.Log10(volume) * 20);
    }

    public void SetMasterVolume()
    {
        /*
        float volume = masterSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        audioMixer.SetFloat("Ambience", Mathf.Log10(volume) * 20);*/
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
