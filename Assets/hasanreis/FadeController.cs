using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadePanel; // Black panel representing the fade
    public float fadeDuration = 1.5f; // Fade duration

    private void Start()
    {
        // When the new scene opens, start with a black screen
        fadePanel.color = new Color(0, 0, 0, 1); // Initially fully black
        fadePanel.gameObject.SetActive(true);
        StartCoroutine(FadeIn()); // Start the FadeIn effect
    }

    public void StartFadeOut(string sceneName)
    {
        fadePanel.gameObject.SetActive(true);  // Activate the panel
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeOut(string sceneName)
    {
        float timer = 0f;

        while (timer <= fadeDuration)
        {
            timer += Time.deltaTime;
            float alphaValue = Mathf.Lerp(0, 1, timer / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, alphaValue);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer <= fadeDuration)
        {
            timer += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1, 0, timer / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, alphaValue);
            yield return null;
        }

        // Deactivate the panel after fading in
        fadePanel.gameObject.SetActive(false);

        // Optionally call a method to handle any post-fade actions
        OnFadeInComplete();
    }

    private void OnFadeInComplete()
    {
        // You can add logic here that you want to execute after fade in is complete.
        // For example, you might want to trigger another fade-out for UI panels or animations.
    }
}
