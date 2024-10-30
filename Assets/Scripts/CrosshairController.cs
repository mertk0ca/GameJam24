using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    private RectTransform crosshairRectTransform;
    private CanvasGroup crosshairCanvasGroup;
    private PlayerShooting playerShooting;

    void Start()
    {
        crosshairRectTransform = GetComponent<RectTransform>();
        crosshairCanvasGroup = GetComponent<CanvasGroup>(); // CanvasGroup bileþenini al
        playerShooting = FindObjectOfType<PlayerShooting>();

        if (playerShooting == null)
        {
            Debug.LogError("PlayerShooting script bulunamadý!");
        }

        // Baþlangýçta opaklýðý 0 yaparak crosshair'i gizle
        crosshairCanvasGroup.alpha = 0;
    }

    void Update()
    {
        if (playerShooting != null && playerShooting.hasWeapon)
        {
            // Opaklýðý 1 yaparak crosshair'i görünür yap
            crosshairCanvasGroup.alpha = 1;

            // Fare pozisyonunu al ve crosshair'ý yerleþtir
            Vector2 mousePosition = Input.mousePosition;
            crosshairRectTransform.position = mousePosition;
        }
        else
        {
            // Opaklýðý 0 yaparak crosshair'i gizle
            crosshairCanvasGroup.alpha = 0;
        }
    }
}
