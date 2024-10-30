using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGravity : MonoBehaviour
{
    public float bulletSpeed = 5f; // Merminin hýzý
    public float bulletLifeTime = 2f; // Merminin kaybolma süresi
    public float rotationSpeed = 360f; // Görselin kendi etrafýnda dönme hýzý
    public GameObject bulletVisual; // Merminin görselinin bulunduðu child nesne
    public int damageAmount = 20; // Düþmana verilecek hasar miktarý

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D bileþenini al

        // Mermiyi belirli bir süre sonra yok et
        Destroy(gameObject, bulletLifeTime);

        // Mouse'un dünya pozisyonunu al ve fýrlat
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized; // Fýrlatma yönü
        rb.velocity = direction * bulletSpeed; // Mermiyi fýrlat
    }

    private void Update()
    {
        // Merminin görselinin kendi etrafýnda dönmesi
        if (bulletVisual != null)
        {
            bulletVisual.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Düþmana çarparsa
        {
            EnemyInfo enemyInfo = collision.GetComponent<EnemyInfo>(); // Düþmanýn EnemyInfo bileþenini al
            if (enemyInfo != null)
            {
                enemyInfo.TakeDamage(damageAmount); // Düþmana hasar ver
            }

            Destroy(gameObject); // Mermiyi yok et
        }

        if (collision.CompareTag("Boss")) // Düþmana çarparsa
        {
            EnemyInfo enemyInfo = collision.GetComponent<EnemyInfo>(); // Düþmanýn EnemyInfo bileþenini al
            if (enemyInfo != null)
            {
                enemyInfo.TakeDamage(damageAmount); // Düþmana hasar ver
            }

            Destroy(gameObject); // Mermiyi yok et
        }

        else if (collision.CompareTag("Breakable")) // Kýrýlabilire çarparsa
        {
            Destroy(collision.gameObject); // Nesneyi yok et
            Destroy(gameObject); // Mermiyi yok et
        }
        else if (!collision.CompareTag("Player") && !collision.CompareTag("Fart"))
        {
            Destroy(gameObject); // Diðer nesnelere çarparsa mermiyi yok et
        }
    }
}