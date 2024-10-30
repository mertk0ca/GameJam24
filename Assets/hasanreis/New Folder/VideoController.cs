using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public Button startButton;
    public Image fadePanel; // Kararma efekti için Panel
    public float fadeDuration = 1f;

    public AudioClip startSound;
    private AudioSource startSource;

    private void Start()
    {
        rawImage.gameObject.SetActive(false);
        fadePanel.gameObject.SetActive(false); // Panel başlangıçta kapalı

        RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);
        videoPlayer.targetTexture = renderTexture;
        rawImage.texture = renderTexture;

        startSource = GetComponent<AudioSource>();

        videoPlayer.loopPointReached += OnVideoFinished;
        startButton.onClick.AddListener(StartFadeEffect); // Başlat tuşuna tıklayınca kararma efektini başlat
    }

    private void StartFadeEffect()
    {
        fadePanel.gameObject.SetActive(true); // Paneli görünür yap
        StartCoroutine(FadeOutAndPlayVideo()); // Kararma efektini başlat

        startSource.clip = startSound;
        startSource.Play();
    }

    private System.Collections.IEnumerator FadeOutAndPlayVideo()
    {
        float timer = 0f;
        Color fadeColor = fadePanel.color;
        fadeColor.a = 1f; // Panelin tamamen opak olmasını sağla
        fadePanel.color = fadeColor;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeColor.a = Mathf.Lerp(1f, 0f, timer / fadeDuration); // Alpha değerini değiştirerek kararma efekti
            fadePanel.color = fadeColor;
            yield return null;
        }

        fadePanel.gameObject.SetActive(false); // Kararma tamamlandıktan sonra paneli gizle
        rawImage.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("SampleScene");
    }
}