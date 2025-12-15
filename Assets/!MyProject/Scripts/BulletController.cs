using UnityEngine;
using UnityEngine.Audio;

public class BulletController : MonoBehaviour
{
    [SerializeField] private string destroyTag = "Destroyable";
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private AudioClip startShootSound;
    [SerializeField] private AudioMixerGroup sfxGroup;

    private bool isDestroyed = false;
    private GameObject gunObject;

    private void Start()
    {
        gunObject = GameObject.FindGameObjectWithTag("Gun");

            Vector3 soundPosition = gunObject != null ? gunObject.transform.position : transform.position;
            PlaySoundWithMixer(startShootSound, soundPosition);

        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(destroyTag) && !isDestroyed)
        {
            isDestroyed = true;

                PlaySoundWithMixer(destroySound, collision.contacts[0].point);

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void PlaySoundWithMixer(AudioClip clip, Vector3 position)
    {
        if (clip == null) return;

        GameObject soundObject = new GameObject("TempAudio_" + clip.name);
        soundObject.transform.position = position;

        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;

        if (sfxGroup != null)
        {
            audioSource.outputAudioMixerGroup = sfxGroup;
        }

        audioSource.Play();
        Destroy(soundObject, clip.length);
    }
}