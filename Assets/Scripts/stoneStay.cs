using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 originalPosition;
    public float fallDelay = 2f; // Platformun d��me s�resi
    public float returnDelay = 1f; // Platformun eski konumuna d�nme s�resi

    private void Start()
    {
        // Rigidbody2D bile�enini al�yoruz
        rb = GetComponent<Rigidbody2D>();
        // Ba�lang�� konumunu kaydediyoruz
        originalPosition = transform.position;
        // Ba�lang��ta platformu hareketsiz hale getiriyoruz
        rb.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Oyuncu platforma de�di�inde
        if (collision.gameObject.CompareTag("Player"))
        {
            // Belirli bir s�re sonra platformu d���rmek i�in Invoke kullan�yoruz
            Invoke("DropPlatform", fallDelay);
        }
    }

    private void DropPlatform()
    {
        // Platformun d��mesini sa�l�yoruz
        rb.isKinematic = false;
        rb.gravityScale = 1; // Platformun d��me h�z�n� kontrol edebilirsiniz

        // Platformu eski konumuna d�nd�rmek i�in geri �a��rma ayarl�yoruz
        Invoke("ResetPlatform", returnDelay);
    }

    private void ResetPlatform()
    {
        // Platformu ba�lang�� konumuna d�nd�r
        rb.isKinematic = true;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero; // Platformun h�z�n� s�f�rla
        transform.position = originalPosition; // Platformun konumunu eski haline getir
    }

    private void OnDisable()
    {
        // Platform yeniden etkinle�tirildi�inde ba�lang�� konumuna d�ns�n
        transform.position = originalPosition;
        rb.isKinematic = true;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        CancelInvoke(); // Di�er gecikmeli �a�r�lar� iptal et
    }
}
