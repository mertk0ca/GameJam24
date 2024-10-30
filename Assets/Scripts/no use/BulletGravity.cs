using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGravity : MonoBehaviour
{
    public float bulletSpeed = 5f; // Merminin h�z�
    public float bulletLifeTime = 2f; // Merminin kaybolma s�resi
    public float rotationSpeed = 360f; // G�rselin kendi etraf�nda d�nme h�z�
    public GameObject bulletVisual; // Merminin g�rselinin bulundu�u child nesne
    public int damageAmount = 20; // D��mana verilecek hasar miktar�

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D bile�enini al

        // Mermiyi belirli bir s�re sonra yok et
        Destroy(gameObject, bulletLifeTime);

        // Mouse'un d�nya pozisyonunu al ve f�rlat
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized; // F�rlatma y�n�
        rb.velocity = direction * bulletSpeed; // Mermiyi f�rlat
    }

    private void Update()
    {
        // Merminin g�rselinin kendi etraf�nda d�nmesi
        if (bulletVisual != null)
        {
            bulletVisual.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // D��mana �arparsa
        {
            EnemyInfo enemyInfo = collision.GetComponent<EnemyInfo>(); // D��man�n EnemyInfo bile�enini al
            if (enemyInfo != null)
            {
                enemyInfo.TakeDamage(damageAmount); // D��mana hasar ver
            }

            Destroy(gameObject); // Mermiyi yok et
        }

        if (collision.CompareTag("Boss")) // D��mana �arparsa
        {
            EnemyInfo enemyInfo = collision.GetComponent<EnemyInfo>(); // D��man�n EnemyInfo bile�enini al
            if (enemyInfo != null)
            {
                enemyInfo.TakeDamage(damageAmount); // D��mana hasar ver
            }

            Destroy(gameObject); // Mermiyi yok et
        }

        else if (collision.CompareTag("Breakable")) // K�r�labilire �arparsa
        {
            Destroy(collision.gameObject); // Nesneyi yok et
            Destroy(gameObject); // Mermiyi yok et
        }
        else if (!collision.CompareTag("Player") && !collision.CompareTag("Fart"))
        {
            Destroy(gameObject); // Di�er nesnelere �arparsa mermiyi yok et
        }
    }
}