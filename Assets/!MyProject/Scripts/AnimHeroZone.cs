using UnityEngine;

public class AnimHeroZone : MonoBehaviour
{
    [Header("Animator Reference")]
    [SerializeField] private Animator animator;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("Right", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("Right", false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("Left", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("Left", false);
        }
    }
}
