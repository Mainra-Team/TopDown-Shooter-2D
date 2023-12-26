using UnityEngine;

public class FollowingBullet : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    public float rotationSpeed = 200f;
    public float disappearDelay = 5f; // Waktu sebelum peluru menghilang

    private Transform targetEnemy;

    private void Start()
    {
        FindNearestEnemy();
        Invoke("Disappear", disappearDelay);
    }

    private void Update()
    {
        if (targetEnemy != null)
        {
            FollowTarget();
        }
        else
        {
            FindNearestEnemy();
        }
    }

    private void FollowTarget()
    {
        Vector3 direction = targetEnemy.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetEnemy = enemy.transform;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Logika untuk menangani dampak peluru ke pemain
            other.GetComponent<Enemy>().TakeDamage(damage);
            DestroyBullet();
        }
        else if (other.CompareTag("Obstacle"))
        {
            // Logika untuk menangani dampak peluru ke hambatan atau objek lain
            DestroyBullet();
        }
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
