using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private static float savedMusicVolume;
    private static float savedSFXVolume;
    private static float savedMasterVolume;

    [Header("Audio Clips")]
    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    [Header("Audio Sources")]
    public AudioSource musicSource;  // For background music
    public AudioSource sfxSource;    // For sound effects

    [Header("Sliders")]
    public Slider musicSlider;      // Slider for music volume
    public Slider sfxSlider;        // Slider for SFX volume
    public Slider masterSlider;

    private void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Adding Audio Clips using Resources.Load
        audioClips.Add("menu", Resources.Load<AudioClip>("main menu music"));
        audioClips.Add("background", Resources.Load<AudioClip>("lofi-relax-travel"));
        audioClips.Add("game-over", Resources.Load<AudioClip>("game-over"));
    }

    private void Start()
    {
        // Load saved volume settings
        savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f); // defaults to 1 if not saved / 1st time
        savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);

        // Set audio and slider values
        musicSource.volume = savedMusicVolume;
        sfxSource.volume = savedSFXVolume;

        // Initialize sliders with the current volumes
        if (musicSlider != null)
        {
            musicSlider.value = savedMusicVolume;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = savedSFXVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        if (masterSlider != null)
        {
            masterSlider.value = savedMasterVolume;
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sliders' events
        if (musicSlider != null)
        {
            musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
        }
    }

    // Play background music
    public void PlayMusic(string clipName)
    {
        if (audioClips.ContainsKey(clipName))
        {
            musicSource.clip = audioClips[clipName];
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music clip not found: " + clipName);
        }
    }

    // Play a sound effect
    public void PlaySFX(string clipName)
    {
        if (audioClips.ContainsKey(clipName))
        {
            sfxSource.PlayOneShot(audioClips[clipName]);
        }
        else
        {
            Debug.LogWarning("SFX clip not found: " + clipName);
        }
    }

    // Stop music
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Stop sound effects
    public void StopSFX()
    {
        sfxSource.Stop();
    }

    // Adjust the volume of music and sound effects separately
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float volume)
    {
        SetSFXVolume(volume);
        SetMusicVolume(volume);
        sfxSlider.value = volume;
        musicSlider.value = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }
}
