using UnityEngine;
using UnityEngine.SceneManagement;  // Sahne değiştirmek için gerekli

public class GamePause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenuUI;  // Canvas panelini referanslayın
    public GameObject pauseButton;  // Duraklatma butonunu referanslayın
    public GameObject resumeButton; // Devam et butonunu referanslayın

    void Start()
    {
        pauseMenuUI.SetActive(false); // Oyunun başlangıcında kapalı olacak
        resumeButton.SetActive(false); // Devam et butonu başlangıçta kapalı olacak
    }

    void Update()
    {
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
    }

    public void PauseGame()
    {
        Time.timeScale = 0;  // Zamanı durdurur
        pauseMenuUI.SetActive(true); // Siyah ekranı ve butonları gösterir
        pauseButton.SetActive(false); // Duraklatma butonunu gizler
        resumeButton.SetActive(true); // Devam et butonunu gösterir
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;  // Zamanı normale döndürür
        pauseMenuUI.SetActive(false); // Siyah ekranı kapatır
        pauseButton.SetActive(true); // Duraklatma butonunu tekrar görünür yapar
        resumeButton.SetActive(false); // Devam et butonunu gizler
        isPaused = false;
    }

    public void PlayVePause()
    {
        if (Time.timeScale == 1)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void LoadMainMenu() // Anasayfaya dön butonu için
    {
        Time.timeScale = 1;  // Zamanı normale döndürür
        SceneManager.LoadScene("MainMenu"); // "MainMenu" adlı sahneyi yükler
    }

    public void QuitGame() // Oyunu kapatma butonu için
    {
        Application.Quit();
    }
}
