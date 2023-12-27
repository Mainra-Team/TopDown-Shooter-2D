using UnityEngine;

public class FollowingBullet : MonoBehaviour
{
    public float rotationSpeed = 200f;

    private Transform targetEnemy;

    private void Start()
    {
        FindNearestEnemy();
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
}
