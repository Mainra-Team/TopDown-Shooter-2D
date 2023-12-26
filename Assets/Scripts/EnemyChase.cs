using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed = 3f;
    public float stoppingDistance = 1f; // Jarak minimum antara musuh dan pemain
    public Transform player;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (player == null)
        {
            // Jika pemain tidak ditemukan, hentikan pengejaran
            return;
        }

        // Hitung vektor arah ke pemain
        Vector3 direction = player.position - transform.position;

        // Hanya lakukan pengejaran jika pemain cukup jauh
        if (direction.magnitude > stoppingDistance)
        {
            // Normalisasi vektor arah untuk menghindari perubahan kecepatan yang tidak diinginkan
            direction.Normalize();

            // Pindahkan musuh menuju pemain menggunakan Rigidbody2D
            rb.velocity = direction * speed;
        }
        else
        {
            // Hentikan pergerakan jika musuh sudah cukup dekat dengan pemain
            rb.velocity = Vector2.zero;
        }
    }
}
