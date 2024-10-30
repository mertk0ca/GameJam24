using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 originalPosition;
    public float fallDelay = 2f; // Platformun düþme süresi
    public float returnDelay = 1f; // Platformun eski konumuna dönme süresi

    private void Start()
    {
        // Rigidbody2D bileþenini alýyoruz
        rb = GetComponent<Rigidbody2D>();
        // Baþlangýç konumunu kaydediyoruz
        originalPosition = transform.position;
        // Baþlangýçta platformu hareketsiz hale getiriyoruz
        rb.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Oyuncu platforma deðdiðinde
        if (collision.gameObject.CompareTag("Player"))
        {
            // Belirli bir süre sonra platformu düþürmek için Invoke kullanýyoruz
            Invoke("DropPlatform", fallDelay);
        }
    }

    private void DropPlatform()
    {
        // Platformun düþmesini saðlýyoruz
        rb.isKinematic = false;
        rb.gravityScale = 1; // Platformun düþme hýzýný kontrol edebilirsiniz

        // Platformu eski konumuna döndürmek için geri çaðýrma ayarlýyoruz
        Invoke("ResetPlatform", returnDelay);
    }

    private void ResetPlatform()
    {
        // Platformu baþlangýç konumuna döndür
        rb.isKinematic = true;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero; // Platformun hýzýný sýfýrla
        transform.position = originalPosition; // Platformun konumunu eski haline getir
    }

    private void OnDisable()
    {
        // Platform yeniden etkinleþtirildiðinde baþlangýç konumuna dönsün
        transform.position = originalPosition;
        rb.isKinematic = true;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        CancelInvoke(); // Diðer gecikmeli çaðrýlarý iptal et
    }
}
