using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZoneTrigger : MonoBehaviour
{
    public GameObject gameOverUI;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f; // Pause game
            gameOverUI.SetActive(true); // Tampilkan UI Game Over
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
