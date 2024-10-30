using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public int maxHealth = 100; // Düþmanýn maksimum caný
    public int currentHealth;   // Düþmanýn mevcut caný

    public Color damageColor = Color.red; // Hasar alýndýðýnda düþmanýn rengi
    private Color originalColor; // Orijinal rengi kaydet
    public float colorChangeDuration = 0.2f; // Rengin deðiþme süresi
    public float invincibilityDuration = 0.5f; // Dokunulmazlýk süresi
    private bool isInvincible = false; // Dokunulmazlýk durumu
    public bool isDead = false; // Düþman öldü mü?

    private SpriteRenderer spriteRenderer; // Düþmanýn SpriteRenderer bileþeni
    public Animator animator; // Düþmanýn Animator bileþeni
    private Rigidbody2D rb; // Düþmanýn Rigidbody2D bileþeni
    private Collider2D enemyCollider; // Düþmanýn Collider bileþeni

    void Start()
    {
        currentHealth = maxHealth; // Baþlangýçta mevcut can maksimum can olur
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer bileþenini al
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D bileþenini al
        enemyCollider = GetComponent<Collider2D>(); // Collider bileþenini al
        originalColor = spriteRenderer.color; // Orijinal rengi kaydet
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer düþman fart objesiyle çarpýþýrsa
        if (other.CompareTag("Fart") && !isInvincible && !isDead) // Eðer ölü deðilse ve invincible deðilse
        {
            TakeDamage(20); // Düþmana hasar ver
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Eðer düþman öldüyse çýk

        currentHealth -= damage; // Düþmanýn canýný azalt
        Debug.Log("Current Health: " + currentHealth); // Mevcut caný göster

        // Düþman öldü mü?
        if (currentHealth <= 0)
        {
            isDead = true; // Düþman öldü
            isInvincible = true; // Düþmaný dokunulmaz yap
            animator.SetBool("isRunning", false); // Eðer hareket animasyonu varsa durdur
            animator.SetBool("isDead", true); // Ölüm animasyonunu baþlat
            rb.velocity = Vector2.zero; // Düþmanýn hýzýný sýfýrla
            enemyCollider.enabled = false; // Collider'ý devre dýþý býrak
            enemyCollider.isTrigger = true; // Collider'ý trigger yap, player içinden geçebilir
            StartCoroutine(DeathCoroutine()); // Ölüm coroutine'ini baþlat
        }
        else
        {
            StartCoroutine(ChangeColorCoroutine()); // Rengini deðiþtir
            StartCoroutine(InvincibilityCoroutine()); // Dokunulmazlýk süresini baþlat
        }
    }

    IEnumerator ChangeColorCoroutine()
    {
        spriteRenderer.color = damageColor; // Rengi kýrmýzý yap
        yield return new WaitForSeconds(colorChangeDuration); // Belirli bir süre bekle
        spriteRenderer.color = originalColor; // Orijinal renge geri dön
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; // Düþmaný dokunulmaz yap
        yield return new WaitForSeconds(invincibilityDuration); // Belirlenen süre kadar bekle
        isInvincible = false; // Tekrar hasar alabilir
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(10f); // 10 saniye bekle
        Destroy(gameObject); // Düþmaný yok et
    }
}
