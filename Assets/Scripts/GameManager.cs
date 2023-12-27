using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;
    public GameObject pausePanel;
    [SerializeField] GameObject howToPlayPanel;
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool isPaused = false;

    private void Awake()
    {
        // Membuat singleton GameManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        startTime = Time.time;
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(true);
            Time.timeScale = 0; // Hentikan waktu
        }
    }

    private void Update()
    {
        UpdateTimer();

        // Mengaktifkan/dememaktifkan panel pause saat tombol Pause di tekan
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        // Menangani input setelah game over
        if (gameOverPanel.activeSelf)
        {
            // Menekan tombol apa pun untuk merestart game
            if (Input.anyKeyDown)
            {
                RestartGame();
            }
        }
    }

    public void GameOver()
    {
        // Menampilkan panel game over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Waktu berhenti (opsional)
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        // Mereload scene saat ini (scene utama)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        // Menampilkan panel pause
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }

        // Waktu berhenti
        Time.timeScale = 0;

        isPaused = true;
    }

    public void ResumeGame()
    {
        // Menyembunyikan panel pause
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        // Waktu dilanjutkan
        Time.timeScale = 1;

        isPaused = false;
    }

    private void UpdateTimer()
    {
        // Hitung selisih waktu dari waktu awal hingga sekarang
        float currentTime = Time.time - startTime;

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

    private void ResetTimer()
    {
        startTime = Time.time;
    }
}
