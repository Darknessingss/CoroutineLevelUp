using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private const float multiplier = 20f;
    private bool isPaused = false;

    private void Start()
    {
        pauseMenuUI.SetActive(false);

        if (musicSlider != null)
        {
            musicSlider.onValueChanged.AddListener(HandleMusicSliderValueChanged);
            SetSliderInitialValue(musicSlider, "MusicVolume");
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(HandleSFXSliderValueChanged);
            SetSliderInitialValue(sfxSlider, "SFXVolume");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    private void SetSliderInitialValue(Slider slider, string volumeParameter)
    {
        float currentVolume;
        if (audioMixer.GetFloat(volumeParameter, out currentVolume))
        {
            slider.value = Mathf.Pow(10, currentVolume / multiplier);
        }
        else
        {
            slider.value = 1f;
        }
    }

    private void HandleMusicSliderValueChanged(float value)
    {
        float volumeValue = Mathf.Log10(value) * multiplier;
        audioMixer.SetFloat("MusicVolume", volumeValue);
    }

    private void HandleSFXSliderValueChanged(float value)
    {
        float volumeValue = Mathf.Log10(value) * multiplier;
        audioMixer.SetFloat("SFXVolume", volumeValue);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}