using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public int maxHealth = 100; // D��man�n maksimum can�
    public int currentHealth;   // D��man�n mevcut can�

    public Color damageColor = Color.red; // Hasar al�nd���nda d��man�n rengi
    private Color originalColor; // Orijinal rengi kaydet
    public float colorChangeDuration = 0.2f; // Rengin de�i�me s�resi
    public float invincibilityDuration = 0.5f; // Dokunulmazl�k s�resi
    private bool isInvincible = false; // Dokunulmazl�k durumu
    public bool isDead = false; // D��man �ld� m�?

    private SpriteRenderer spriteRenderer; // D��man�n SpriteRenderer bile�eni
    public Animator animator; // D��man�n Animator bile�eni
    private Rigidbody2D rb; // D��man�n Rigidbody2D bile�eni
    private Collider2D enemyCollider; // D��man�n Collider bile�eni

    void Start()
    {
        currentHealth = maxHealth; // Ba�lang��ta mevcut can maksimum can olur
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer bile�enini al
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D bile�enini al
        enemyCollider = GetComponent<Collider2D>(); // Collider bile�enini al
        originalColor = spriteRenderer.color; // Orijinal rengi kaydet
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // E�er d��man fart objesiyle �arp���rsa
        if (other.CompareTag("Fart") && !isInvincible && !isDead) // E�er �l� de�ilse ve invincible de�ilse
        {
            TakeDamage(20); // D��mana hasar ver
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // E�er d��man �ld�yse ��k

        currentHealth -= damage; // D��man�n can�n� azalt
        Debug.Log("Current Health: " + currentHealth); // Mevcut can� g�ster

        // D��man �ld� m�?
        if (currentHealth <= 0)
        {
            isDead = true; // D��man �ld�
            isInvincible = true; // D��man� dokunulmaz yap
            animator.SetBool("isRunning", false); // E�er hareket animasyonu varsa durdur
            animator.SetBool("isDead", true); // �l�m animasyonunu ba�lat
            rb.velocity = Vector2.zero; // D��man�n h�z�n� s�f�rla
            enemyCollider.enabled = false; // Collider'� devre d��� b�rak
            enemyCollider.isTrigger = true; // Collider'� trigger yap, player i�inden ge�ebilir
            StartCoroutine(DeathCoroutine()); // �l�m coroutine'ini ba�lat
        }
        else
        {
            StartCoroutine(ChangeColorCoroutine()); // Rengini de�i�tir
            StartCoroutine(InvincibilityCoroutine()); // Dokunulmazl�k s�resini ba�lat
        }
    }

    IEnumerator ChangeColorCoroutine()
    {
        spriteRenderer.color = damageColor; // Rengi k�rm�z� yap
        yield return new WaitForSeconds(colorChangeDuration); // Belirli bir s�re bekle
        spriteRenderer.color = originalColor; // Orijinal renge geri d�n
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; // D��man� dokunulmaz yap
        yield return new WaitForSeconds(invincibilityDuration); // Belirlenen s�re kadar bekle
        isInvincible = false; // Tekrar hasar alabilir
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(10f); // 10 saniye bekle
        Destroy(gameObject); // D��man� yok et
    }
}
