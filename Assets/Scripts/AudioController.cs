using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [Header("Audio Mixer Groups")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup effectsGroup;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip gameOverSoundWithLowScore;
    public AudioClip gameOverSoundWithHighScore;
    public AudioClip damageSound;
    public AudioClip destroyEnemySound;
    public AudioClip healingPickupSound;

    private AudioSource musicSource;
    private AudioSource effectsSource;


    void Awake() {
        // Singleton pattern
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        // Create and configure AudioSources
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.outputAudioMixerGroup = musicGroup;
        musicSource.loop = true;

        effectsSource = gameObject.AddComponent<AudioSource>();
        effectsSource.outputAudioMixerGroup = effectsGroup;
    }

    public void PlayBackgroundMusic() {
        if (backgroundMusic != null) {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    public void PlayEffect(AudioClip clip)
    {
        if (clip != null) {
            effectsSource.PlayOneShot(clip);
        }
    }

    public void SetMusicVolume(float volume) {
        musicGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); // Convert to dB
    }

    public void SetEffectsVolume(float volume) {
        effectsGroup.audioMixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);
    }
}
