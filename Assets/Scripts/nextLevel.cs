using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class nextLevel : MonoBehaviour
{
    public string nextSceneName;
    public AudioClip urartuClip;
    public AudioClip backgroundMusic;

    private AudioSource audioSource;
    private AudioSource backgroundAudioSource;

    public Image papirus;
    public Image black;
    public Image star;

    public float waitTime = 15f;
    public float fadeDuration = 2f; // Hem fade out hem fade in süresi

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        backgroundAudioSource = GetComponent<AudioSource>();

        backgroundAudioSource.clip = backgroundMusic;
        backgroundAudioSource.volume = 0.15f;
        backgroundAudioSource.Play();

        papirus.enabled = false;
        star.enabled = false;
        black.enabled = true; // Black görüntüsü sahne baþýnda aktif olacak

        StartCoroutine(FadeIn()); // Sahne baþlangýcýnda fade in efekti baþlatýlýyor
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.clip = urartuClip;
            audioSource.Play();

            papirus.enabled = true;
            star.enabled = true;
            black.enabled = true;

            Time.timeScale = 0f;

            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        StartCoroutine(FadeOut()); // Fade out efektini baþlat
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color blackColor = black.color;
        blackColor.a = 1f;
        black.color = blackColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            blackColor.a = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
            black.color = blackColor;
            yield return null;
        }

        black.enabled = false; // Fade in tamamlandýðýnda siyah görüntüyü gizle
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color blackColor = black.color;
        blackColor.a = 0f;
        black.color = blackColor;
        papirus.enabled = false;
        star.enabled = false;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            blackColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            black.color = blackColor;
            yield return null;
        }

        // Fade out tamamlandýktan sonra sahne geçiþi yapýlýr
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }
}
