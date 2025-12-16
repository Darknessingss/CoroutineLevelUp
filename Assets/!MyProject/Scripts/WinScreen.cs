using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private bool pauseGame = true;
    [SerializeField] private bool showCursor = true;

    private AudioSource audioSource;
    private bool alreadyWon = false;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;

        if (winScreen != null)
            winScreen.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !alreadyWon)
        {
            WinGame();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !alreadyWon)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        alreadyWon = true;

        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }

        if (winSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(winSound);
        }

        if (pauseGame)
        {
            Time.timeScale = 0f;
        }

        if (showCursor)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}