using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    private void Start()
    {
        Destroy(gameObject, 5f); // Hancurkan proyektil setelah 5 detik
    }

    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah proyektil bertabrakan dengan objek yang memiliki tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Dapatkan komponen Health dari objek yang tertabrak
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            // Jika objek memiliki komponen EnemyHealth, berikan damage ke musuh
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Hancurkan proyektil setelah menyentuh musuh
            Destroy(gameObject);
        }
    }
}
