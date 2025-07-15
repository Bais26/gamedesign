using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalCoins; // Jumlah total coin di level ini
    public int coinsCollected = 0;

    public GameObject popupLevelLocked;    // Panel: "Kumpulkan semua koin!"
    public GameObject popupGameComplete;   // Panel: "Selamat, kamu menamatkan game!"

    private string currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void AddCoin()
    {
        coinsCollected++;
    }

    public void TryGoToNextLevel()
    {
        if (coinsCollected >= totalCoins)
        {
            if (currentScene == "level3")
            {
                // TAMAT
                popupGameComplete.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                SceneManager.LoadScene(GetNextLevelName());
            }
        }
        else
        {
            // Koin belum cukup
            popupLevelLocked.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    string GetNextLevelName()
    {
        if (currentScene == "bais_v1_1") return "level2";
        if (currentScene == "level2") return "level3";
        return "";
    }
    public void ClosePopupLocked()
{
    popupLevelLocked.SetActive(false);
    Time.timeScale = 1f; // Kembali jalan normal
}
}
