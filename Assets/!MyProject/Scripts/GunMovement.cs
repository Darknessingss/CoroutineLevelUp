using UnityEngine;

public class GunMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.Z))
        {
            movement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.X))
        {
            movement += Vector3.back;
        }

        if (movement != Vector3.zero)
        {
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }
}