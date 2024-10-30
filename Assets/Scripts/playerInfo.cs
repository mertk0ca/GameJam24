using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerInfo : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float knockbackForce = 5f; // geri savrulma kuvveti
    public float invincibilityDuration = 1f; // çarpma sonrasý dokunulmazlýk süresi
    private bool isInvincible = false; // oyuncu geçici olarak zarar görmez

    public Color damageColor = Color.red; // hasar alýnca kýrmýzý yap
    private Color originalColor; // orijinal rengi kaydet
    public float colorChangeDuration = 0.2f; // kýrmýzý kalma süresi

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public AudioClip hurtSound;
    private AudioSource hurtSource;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // karakterin orijinal rengi
        hurtSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Düþmanýn EnemyInfo bileþenini al
        EnemyInfo enemyInfo = collision.gameObject.GetComponent<EnemyInfo>();

        // Eðer düþmanla çarpýþýyorsak ve düþman ölmemiþse hasar al
        if (collision.gameObject.CompareTag("Enemy") && enemyInfo != null && !enemyInfo.isDead && !isInvincible)
        {
            TakeDamage(20, collision.transform);
            hurtSource.clip = hurtSound;
            hurtSource.Play();
        }

        if (collision.gameObject.CompareTag("Boss") && enemyInfo != null && !enemyInfo.isDead && !isInvincible)
        {
            TakeDamage(50, collision.transform);
        }
    }

    void TakeDamage(int damage, Transform enemyTransform)
    {
        currentHealth -= damage; // caný azalt
        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0) // can sýfýrdan küçükse öl
        {
            Die();
        }
        else
        {
            Vector2 knockbackDirection = (transform.position - enemyTransform.position); // normalize edilmiþ yön
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse); // kuvveti uygula

            // oyuncuyu dokunulmaz yap ve rengini deðiþtir
            StartCoroutine(InvincibilityCoroutine());
            StartCoroutine(ChangeColorCoroutine());
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; // oyuncuyu dokunulmaz yap
        yield return new WaitForSeconds(invincibilityDuration); // belirlenen süre kadar bekle
        isInvincible = false; // tekrar hasar alabilir
    }

    IEnumerator ChangeColorCoroutine()
    {
        spriteRenderer.color = damageColor; // kýrmýzý yap
        yield return new WaitForSeconds(colorChangeDuration); // belirli bir süre kýrmýzý kalsýn
        spriteRenderer.color = originalColor; // eski renge geri dön
    }

    void Die() // ölme
    {
        Debug.Log("Player Died!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
