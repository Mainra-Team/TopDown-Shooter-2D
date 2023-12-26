using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    private int damage = 10; // Atur damage sesuai kebutuhan

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Mengecek apakah objek yang terkena peluru memiliki komponen EnemyHealth
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // Berikan damage kepada musuh
                enemyHealth.TakeDamage(damage);
            }

            // Hancurkan peluru setelah menyentuh objek
            Destroy(gameObject);
        }
    }

    // Fungsi untuk mengatur damage dari luar skrip
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
