using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] private GameObject door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && door != null)
        {
            Collider2D doorCollider = door.GetComponent<Collider2D>();
            if (doorCollider != null)
            {
                doorCollider.isTrigger = true;
            }
        }
    }
}