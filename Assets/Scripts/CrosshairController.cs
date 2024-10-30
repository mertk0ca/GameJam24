using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    private RectTransform crosshairRectTransform;
    private CanvasGroup crosshairCanvasGroup;
    private PlayerShooting playerShooting;

    void Start()
    {
        crosshairRectTransform = GetComponent<RectTransform>();
        crosshairCanvasGroup = GetComponent<CanvasGroup>(); // CanvasGroup bile�enini al
        playerShooting = FindObjectOfType<PlayerShooting>();

        if (playerShooting == null)
        {
            Debug.LogError("PlayerShooting script bulunamad�!");
        }

        // Ba�lang��ta opakl��� 0 yaparak crosshair'i gizle
        crosshairCanvasGroup.alpha = 0;
    }

    void Update()
    {
        if (playerShooting != null && playerShooting.hasWeapon)
        {
            // Opakl��� 1 yaparak crosshair'i g�r�n�r yap
            crosshairCanvasGroup.alpha = 1;

            // Fare pozisyonunu al ve crosshair'� yerle�tir
            Vector2 mousePosition = Input.mousePosition;
            crosshairRectTransform.position = mousePosition;
        }
        else
        {
            // Opakl��� 0 yaparak crosshair'i gizle
            crosshairCanvasGroup.alpha = 0;
        }
    }
}
