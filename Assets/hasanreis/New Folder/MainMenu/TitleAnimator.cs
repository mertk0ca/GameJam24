using UnityEngine;
using TMPro;
using System.Collections;

public class TitleAnimator : MonoBehaviour
{
    public TextMeshProUGUI gameTitle; // TextMeshPro bileşeni için
    public float minFontSize = 30f; // Minimum font boyutu
    public float maxFontSize = 40f; // Maksimum font boyutu
    public float minCharacterSpacing = 0f; // Minimum harf aralığı
    public float maxCharacterSpacing = 10f; // Maksimum harf aralığı
    public float speed = 0.5f; // Büyütme/küçültme hızı
    public float waitTime = 2f; // Bekleme süresi

    void Start()
    {
        // RectTransform pivot noktasını ortalayarak büyümenin her iki yöne de eşit olmasını sağlar
        RectTransform rectTransform = gameTitle.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        
        // Animasyonu başlat
        StartCoroutine(AnimateTitle());
    }

    private IEnumerator AnimateTitle()
    {
        while (true)
        {
            // Harf aralığını minimum ve maksimum değer arasında salınım yaptırır
            float t = 0f;

            // Harf aralığını artırma kısmı
            while (t < 1f)
            {
                t += Time.deltaTime * speed; // Zamanı güncelle
                float smoothStep = Mathf.SmoothStep(0f, 1f, t); // Yumuşak geçiş
                float characterSpacing = Mathf.Lerp(minCharacterSpacing, maxCharacterSpacing, smoothStep);
                gameTitle.characterSpacing = characterSpacing;
                float fontSize = Mathf.Lerp(minFontSize, maxFontSize, smoothStep);
                gameTitle.fontSize = fontSize;

                // TextMeshPro bileşeninin boyutunu güncelle
                gameTitle.GetComponent<RectTransform>().sizeDelta = new Vector2(gameTitle.preferredWidth, gameTitle.preferredHeight);

                yield return null; // Bir frame bekle
            }

            // Harf aralığını tekrar minimum boyuta döndürme
            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * speed; // Zamanı güncelle
                float smoothStep = Mathf.SmoothStep(0f, 1f, t); // Yumuşak geçiş
                float characterSpacing = Mathf.Lerp(maxCharacterSpacing, minCharacterSpacing, smoothStep);
                gameTitle.characterSpacing = characterSpacing;
                float fontSize = Mathf.Lerp(maxFontSize, minFontSize, smoothStep);
                gameTitle.fontSize = fontSize;

                // TextMeshPro bileşeninin boyutunu güncelle
                gameTitle.GetComponent<RectTransform>().sizeDelta = new Vector2(gameTitle.preferredWidth, gameTitle.preferredHeight);

                yield return null; // Bir frame bekle
            }

            // Minimum aralığa ulaştıktan sonra 2 saniye bekle
            yield return new WaitForSeconds(waitTime);
        }
    }
}
