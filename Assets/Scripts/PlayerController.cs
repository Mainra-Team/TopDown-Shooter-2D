using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public float shootCooldown = 0.5f;
    public GameObject bulletPrefabNormal;
    public GameObject bulletPrefabUltimate;
    public Transform bulletSpawnPoint;
    public int bulletTotal;
    public Transform weapon;

    private AudioSource shootingAudioSource; // Komponen AudioSource untuk efek suara tembakan
    public AudioClip normalShotClip;       // AudioClip untuk tembakan normal
    public AudioClip ultimateShotClip;     // AudioClip untuk tembakan ultimate

    private int currentHealth;
    private float shootTimer;

    public Slider healthSlider;
    private Animator playerAnimator;

    private void Start()
    {
        currentHealth = maxHealth;
        bulletTotal = 1;

        // Set nilai awal slider sesuai dengan maxHealth
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        // Mendapatkan komponen Animator dari GameObject
        playerAnimator = GetComponent<Animator>();
        // Mendapatkan komponen AudioSource
        shootingAudioSource = GetComponent<AudioSource>();
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

        // Set nilai variabel animator berdasarkan arah gerakan
        playerAnimator.SetBool("Idle", horizontalInput == 0);
        playerAnimator.SetFloat("HorizontalInput", horizontalInput);
        playerAnimator.SetFloat("VerticalInput", verticalInput);
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
            ShootUltimate();
            shootTimer = 0f;
        }
    }

    public void IncreaseBulletTotal(int value)
    {
        bulletTotal += value;
    }

    public void IncreaseHealth(int value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
        healthSlider.value = currentHealth;
    }

    private void ShootNormal()
    {
        // Memutar efek suara tembakan normal
        if (shootingAudioSource != null && normalShotClip != null)
        {
            shootingAudioSource.clip = normalShotClip;
            shootingAudioSource.Play();
        }
        for (int i = 0; i < bulletTotal; i++)
        {
            // Perhitungan sudut spread berpusat pada kursor
            float spreadAngle = (i - (bulletTotal - 1) / 2f) * 15;

            // Quaternion untuk rotasi tembakan
            Quaternion spreadRotation = Quaternion.Euler(0f, 0f, spreadAngle);

            // Instantiate tembakan
            Instantiate(bulletPrefabNormal, bulletSpawnPoint.position, weapon.rotation * spreadRotation);
        }
    }

    private void ShootUltimate()
    {
        // Memutar efek suara tembakan ultimate
        if (shootingAudioSource != null && ultimateShotClip != null)
        {
            shootingAudioSource.clip = ultimateShotClip;
            shootingAudioSource.Play();
        }

        Instantiate(bulletPrefabUltimate, bulletSpawnPoint.position, weapon.rotation);
    }

    // Fungsi untuk dipanggil oleh slider untuk menyinkronkan nilai health
    public void SetHealthFromSlider(float newHealth)
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
        currentHealth = Mathf.Max(currentHealth - damage, 0);

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
        GameManager.instance.GameOver();
    }
}
