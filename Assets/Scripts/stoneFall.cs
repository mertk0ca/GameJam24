using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneFall : MonoBehaviour
{
    public float fallDelay = 3f; // Taþýn düþtükten sonra ýþýnlanmasý için geçen süre
    private Vector2 initialPosition;
    private bool isFalling = false; // Coroutine'in aktif olup olmadýðýný kontrol eder

    private void Start()
    {
        // Baþlangýç pozisyonunu kaydet
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        // Coroutine'in baþlatýlmasýný kontrol et
        if (!isFalling)
        {
            StartCoroutine(FallAndReset());
        }
    }

    private IEnumerator FallAndReset()
    {
        isFalling = true; // Coroutine'in aktif olduðunu iþaretle

        // Belirtilen süre kadar bekle
        yield return new WaitForSeconds(fallDelay);

        // Taþý baþlangýç konumuna ýþýnla
        transform.position = initialPosition;

        // Collider'ý tekrar etkinleþtir
        GetComponent<Collider2D>().enabled = true;

        // Coroutine'i tekrar baþlatmak için script'i devre dýþý býrakýp tekrar etkinleþtir
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        isFalling = false; // Coroutine tamamlandýðýnda durumu sýfýrla
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Eðer çarpýlan obje "enemy" tag'ine sahipse iþlem yapma ve collider'ý devre dýþý býrak
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Collider2D>().enabled = false;
            return;
        }

        // Eðer coroutine zaten çalýþýyorsa tekrar baþlatma
        if (!isFalling)
        {
            StartCoroutine(FallAndReset());
        }
    }
}
