using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public Transform target; // Pemain sebagai target

    private NavMeshAgent navMeshAgent;
    private bool isFacingRight = true;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (target != null)
        {
            navMeshAgent.SetDestination(target.position);
            transform.localRotation = quaternion.identity;

            // Mengubah arah sprite berdasarkan velocity
            Vector3 velocity = navMeshAgent.velocity;
            if (velocity.x > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (velocity.x < 0 && isFacingRight)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        // Flip sprite horizontal
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
