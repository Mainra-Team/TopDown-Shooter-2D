using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public bool isGameOver = false; // Setel menjadi true ketika game over

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (isGameOver)
        {
            // Reset timer ketika game over
            ResetTimer();
        }
        else
        {
            float currentTime = Time.time;

            // Menghitung menit dan detik
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);

            // Format waktu menjadi "00:00"
            string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

            // Menetapkan teks pada TextMeshPro
            if (timerText != null)
            {
                timerText.text = timerString;
            }
        }
    }

    private void ResetTimer()
    {
        // Mengatur ulang waktu ke 0:00 saat game over
        if (timerText != null)
        {
            timerText.text = "00:00";
        }
    }
}
