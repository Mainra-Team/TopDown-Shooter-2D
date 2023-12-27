using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        Weapon,
        Health
        // Tambahkan jenis PowerUp lainnya jika diperlukan
    }

    [SerializeField] private PowerUpType powerUpType = PowerUpType.Weapon;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                ApplyPowerUp(playerController);
                Destroy(gameObject);
            }
        }
    }

    void ApplyPowerUp(PlayerController player)
    {
        switch (powerUpType)
        {
            case PowerUpType.Weapon:
                player.IncreaseBulletTotal(1);
                break;
            case PowerUpType.Health:
                player.IncreaseHealth(10);
                break;
            // Tambahkan case untuk jenis PowerUp lainnya jika diperlukan
            default:
                Debug.LogWarning("Unknown PowerUp type: " + powerUpType);
                break;
        }
    }
}
