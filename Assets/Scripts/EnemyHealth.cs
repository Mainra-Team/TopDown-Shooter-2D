using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
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
