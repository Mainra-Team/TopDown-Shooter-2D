using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int attackDamage = 10;

    private int currentHealth;

    public Slider healthSlider;
    public Image healthFillImage; // Tambahkan referensi ke komponen Image

    public Color lowHealthColor = Color.red; // Warna ketika kesehatan rendah

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        // Dapatkan komponen Image dari slider
        if (healthSlider != null)
        {
            healthFillImage = healthSlider.fillRect.GetComponent<Image>();
        }
    }

    private void UpdateHealthColor()
    {
        // Ubah warna fill image berdasarkan kondisi tertentu (misalnya, kesehatan kurang dari atau sama dengan 50)
        if (currentHealth <= 50)
        {
            healthFillImage.color = lowHealthColor;
        }
        else
        {
            // Kembalikan warna asli jika kesehatan di atas 50
            healthFillImage.color = Color.green;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(attackDamage);
            }
            Destroy(gameObject, .5f);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;

            // Perbarui warna health fill image
            UpdateHealthColor();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
