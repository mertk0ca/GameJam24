using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public FadeController fadeController; // FadeController referansı

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            collision.gameObject.SetActive(false);

            // Fade efektini başlatır ve sahneyi değiştirir
            fadeController.StartFadeOut("NextScene");
        }
    }
}
