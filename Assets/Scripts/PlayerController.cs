using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public float shootCooldown = 0.5f;
    public GameObject bulletPrefabNormal;
    public GameObject bulletPrefabTripleShot;
    public GameObject bulletPrefabBurst;
    public Transform bulletSpawnPoint;
    public Transform weapon;
    public int maxHealth = 100;

    private int currentHealth;
    private float shootTimer;

    public Slider healthSlider;

    private void Start()
    {
        currentHealth = maxHealth;

        // Set nilai awal slider sesuai dengan maxHealth
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    private void Update()
    {
        MovePlayer();
        RotateWeapon();
        HandleShooting();
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void RotateWeapon()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - weapon.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        weapon.rotation = Quaternion.RotateTowards(weapon.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleShooting()
    {
        shootTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && shootTimer >= shootCooldown)
        {
            ShootNormal();
            shootTimer = 0f;
        }

        if (Input.GetMouseButtonDown(1) && shootTimer >= shootCooldown)
        {
            ShootMultiple();
            shootTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Q) && shootTimer >= shootCooldown)
        {
            ShootBurst();
            shootTimer = 0f;
        }
    }

    private void ShootNormal()
    {
        Instantiate(bulletPrefabNormal, bulletSpawnPoint.position, weapon.rotation);
    }

    private void ShootMultiple()
    {
        for (int i = 0; i < 3; i++)
        {
            Quaternion spreadRotation = Quaternion.Euler(0f, 0f, i * 15f - 15f);
            Instantiate(bulletPrefabTripleShot, bulletSpawnPoint.position, weapon.rotation * spreadRotation);
        }
    }

    private void ShootBurst()
    {
        Instantiate(bulletPrefabBurst, bulletSpawnPoint.position, weapon.rotation);
    }

    // Fungsi untuk dipanggil oleh slider untuk menyinkronkan nilai health
    private void SetHealthFromSlider(float newHealth)
    {
        // Pastikan bahwa nilai health yang diatur tidak lebih besar dari maxHealth atau lebih kecil dari 0
        currentHealth = Mathf.Clamp((int)newHealth, 0, maxHealth);

        // Set nilai slider sesuai dengan nilai health yang baru
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    // Fungsi untuk mengurangi health saat terkena serangan
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Pastikan bahwa nilai health tidak lebih kecil dari 0
        currentHealth = Mathf.Max(currentHealth, 0);

        // Set nilai slider sesuai dengan nilai health yang baru
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // Handle kematian jika health habis
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        Time.timeScale = 0;
    }
}
