using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private AudioMixerGroup sfxGroup;

    [Header("Звуки оружия")]
    [SerializeField] private AudioClip[] shootSounds;
    [SerializeField] private AudioClip[] hitSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySpatialSound(AudioClip clip, Vector3 position, AudioMixerGroup group = null)
    {
        GameObject soundObj = new GameObject("TempAudio");
        soundObj.transform.position = position;

        AudioSource audioSource = soundObj.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.spatialBlend = 0.5f;
        audioSource.maxDistance = 1f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;

        if (group != null)
        {
            audioSource.outputAudioMixerGroup = group;
        }

        audioSource.Play();
        Destroy(soundObj, clip.length);
    }
    public void PlayRandomShootSound(Vector3 position)
    {
        if (shootSounds.Length > 0)
        {
            AudioClip clip = shootSounds[Random.Range(0, shootSounds.Length)];
            PlaySpatialSound(clip, position, sfxGroup);
        }
    }
}