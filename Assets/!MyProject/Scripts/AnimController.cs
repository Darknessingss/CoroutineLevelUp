using UnityEngine;

public class AnimController : MonoBehaviour
{
    [Header("Animator Reference")]
    [SerializeField] private Animator animator;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("AnimRun1", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("AnimRun1", false);
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("AnimRun2", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("AnimRun2", false);
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("AnimRun3", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("AnimRun3", false);
        }
    }
}
