using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startBtn;
    public Player birdBek;

    public Text gameOverTxt;
    public Text scoreTxt; // Reference to the score UI text
    public float timerBack = 5;

    // Reference to the AudioManager
    public AudioManager audioManager;

    private int score = 0; // The player's score
    private bool deathSoundPlayed = false;
    private bool musicStopped = false;

    private void Start()
    {
        gameOverTxt.gameObject.SetActive(false);
        scoreTxt.text = "Score: 0"; // Initialize the score text
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (birdBek.isDead)
        {
            gameOverTxt.gameObject.SetActive(true);
            timerBack -= Time.unscaledDeltaTime; // Start counting back

            // Play death sound if not already played
            if (!deathSoundPlayed)
            {
                audioManager.PlaySFX(audioManager.death);
                deathSoundPlayed = true; // Mark the sound as played
            }

            // Stop the background music if not already stopped
            if (!musicStopped)
            {
                audioManager.StopMusic(); // Use the method from AudioManager
                musicStopped = true; // Mark the music as stopped
            }
        }

        gameOverTxt.text = "Restarting in " + timerBack.ToString("0");

        if (timerBack < 0)
        {
            RestartGame();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreTxt.text = "Score: " + score; // Update the score display
    }

    public void StartGame()
    {
        startBtn.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        birdBek.GetComponent<Rigidbody2D>().simulated = true;
        birdBek.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; // Remove constraints
        EditorSceneManager.LoadScene(0); // Reload the scene to its initial state
    }
}