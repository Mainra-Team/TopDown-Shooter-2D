using UnityEngine;

public class FollowingBullet : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    public float rotationSpeed = 200f;
    public float disappearDelay = 5f; // Waktu sebelum peluru menghilang

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Enemy").transform;
        Invoke("Disappear", disappearDelay);
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (player == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Logika untuk menangani dampak peluru ke pemain
            other.GetComponent<BulletDamage>().SetDamage(damage);
        }
        else if (other.CompareTag("Obstacle"))
        {
            // Logika untuk menangani dampak peluru ke hambatan atau objek lain
        }

        // Hancurkan bullet setelah menyentuh objek apapun
        DestroyBullet();
    }

    private void Disappear()
    {
        // Hancurkan bullet setelah waktu disappearDelay
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
