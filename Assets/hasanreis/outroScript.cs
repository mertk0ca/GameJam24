using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class outroScript : MonoBehaviour
{
    public VideoPlayer outroVideoPlayer;
    public RawImage rawImage;
    public Image fadePanel; // Kararma efekti i�in panel
    public float fadeDuration = 1f; // Kararma s�resi

    private void Start()
    {
        rawImage.gameObject.SetActive(true); // Video g�sterilece�i i�in RawImage aktif
        fadePanel.gameObject.SetActive(false); // Panel ba�lang��ta kapal�

        RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);
        outroVideoPlayer.targetTexture = renderTexture;
        rawImage.texture = renderTexture;

        outroVideoPlayer.loopPointReached += OnVideoFinished; // Video bitti�inde sahneyi y�kle
        outroVideoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        StartCoroutine(FadeToMainMenu()); // Video bitti�inde kararma efekti ile ana men�ye ge�i� yap
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
            fadeColor.a = Mathf.Clamp01(timer / fadeDuration); // Kararma efekti i�in alpha de�eri art�r�l�r
            fadePanel.color = fadeColor;
            yield return null;
        }

        SceneManager.LoadScene("MainMenu"); // Ana men�ye ge�i�
    }
}
