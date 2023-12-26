using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int attackDamage = 10;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Panggil fungsi TakeDamage pada script PlayerController
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(attackDamage);
            }
            Destroy(gameObject);

        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Tambahkan logika kematian musuh di sini, misalnya, efek animasi, poin, atau memusnahkan objek
        Destroy(gameObject);
    }
}
