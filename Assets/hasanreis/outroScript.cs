using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class outroScript : MonoBehaviour
{
    public VideoPlayer outroVideoPlayer;
    public RawImage rawImage;
    public Image fadePanel; // Kararma efekti için panel
    public float fadeDuration = 1f; // Kararma süresi

    private void Start()
    {
        rawImage.gameObject.SetActive(true); // Video gösterileceði için RawImage aktif
        fadePanel.gameObject.SetActive(false); // Panel baþlangýçta kapalý

        RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);
        outroVideoPlayer.targetTexture = renderTexture;
        rawImage.texture = renderTexture;

        outroVideoPlayer.loopPointReached += OnVideoFinished; // Video bittiðinde sahneyi yükle
        outroVideoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        StartCoroutine(FadeToMainMenu()); // Video bittiðinde kararma efekti ile ana menüye geçiþ yap
    }

    private System.Collections.IEnumerator FadeToMainMenu()
    {
        fadePanel.gameObject.SetActive(true);
        float timer = 0f;
        Color fadeColor = fadePanel.color;
        fadeColor.a = 0f;
        fadePanel.color = fadeColor;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(timer / fadeDuration); // Kararma efekti için alpha deðeri artýrýlýr
            fadePanel.color = fadeColor;
            yield return null;
        }

        SceneManager.LoadScene("MainMenu"); // Ana menüye geçiþ
    }
}
