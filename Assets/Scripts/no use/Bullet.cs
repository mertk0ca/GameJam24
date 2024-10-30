using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f; // Merminin h�z�
    public float bulletLifeTime = 2f; // Merminin kaybolma s�resi
    public float rotationSpeed = 360f; // G�rselin kendi etraf�nda d�nme h�z�
    public GameObject bulletVisual; // Merminin g�rselinin bulundu�u child nesne
    public int damageAmount = 20; // D��mana verilecek hasar miktar�

    private void Start()
    {
        // Mermiyi belirli bir s�re sonra yok et
        Destroy(gameObject, bulletLifeTime);
    }

    private void Update()
    {
        // Merminin hareket etmesi
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);

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
