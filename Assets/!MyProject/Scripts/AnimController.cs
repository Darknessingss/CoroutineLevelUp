using UnityEngine;

public class AnimController : MonoBehaviour
{
    [Header("Animator Reference")]
    [SerializeField] private Animator animator;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.GetBool("Anim_Run_Left");
        }
    }
}
