using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMoves : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Camera playerCamera;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float fireRate = 0.5f; 

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float damageFromDestroyable = 20f;

    [Header("UI References")]
    [SerializeField] private Slider healthSlider;

    private Vector2 moveInput;
    private bool onGround;
    private float lastDamageTime;

    [SerializeField] private Rigidbody rb;
    private float nextFireTime;

    private void Start()
    {
        
       currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        ReadInputs();
        HandleJump();
        UpdateHealthUI();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void ReadInputs()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
    }
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }
    private void Move()
    {
        if (playerCamera == null) return;

        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        Vector3 direction = (right * moveInput.x + forward * moveInput.y).normalized;
        direction.y = 0f;

        rb.linearVelocity = new Vector3(direction.x * speed, rb.linearVelocity.y, direction.z * speed);
    }
    private void OnCollisionEnter(Collision col)
    {

        if (col.collider.CompareTag("Ground") || col.collider.CompareTag("Destroyable"))
        {
            onGround = true;
        }

        if (col.collider.CompareTag("Destroyable"))
        {
            TakeDamage(damageFromDestroyable);
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.collider.CompareTag("Ground") || col.collider.CompareTag("Destroyable"))
        {
            onGround = false;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        RestartGame();
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}