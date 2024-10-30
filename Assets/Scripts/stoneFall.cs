using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneFall : MonoBehaviour
{
    public float fallDelay = 3f; // Ta��n d��t�kten sonra ���nlanmas� i�in ge�en s�re
    private Vector2 initialPosition;
    private bool isFalling = false; // Coroutine'in aktif olup olmad���n� kontrol eder

    private void Start()
    {
        // Ba�lang�� pozisyonunu kaydet
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        // Coroutine'in ba�lat�lmas�n� kontrol et
        if (!isFalling)
        {
            StartCoroutine(FallAndReset());
        }
    }

    private IEnumerator FallAndReset()
    {
        isFalling = true; // Coroutine'in aktif oldu�unu i�aretle

        // Belirtilen s�re kadar bekle
        yield return new WaitForSeconds(fallDelay);

        // Ta�� ba�lang�� konumuna ���nla
        transform.position = initialPosition;

        // Collider'� tekrar etkinle�tir
        GetComponent<Collider2D>().enabled = true;

        // Coroutine'i tekrar ba�latmak i�in script'i devre d��� b�rak�p tekrar etkinle�tir
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        isFalling = false; // Coroutine tamamland���nda durumu s�f�rla
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // E�er �arp�lan obje "enemy" tag'ine sahipse i�lem yapma ve collider'� devre d��� b�rak
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Collider2D>().enabled = false;
            return;
        }

        // E�er coroutine zaten �al���yorsa tekrar ba�latma
        if (!isFalling)
        {
            StartCoroutine(FallAndReset());
        }
    }
}
