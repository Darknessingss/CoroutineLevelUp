using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private string destroyTag = "Destroyable";
    [SerializeField] private float lifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Destroyable"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Destroyable"))
        {
            Destroy(collision.gameObject, 2);
        }
    }
}