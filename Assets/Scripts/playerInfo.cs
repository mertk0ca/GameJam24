using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerInfo : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float knockbackForce = 5f; // geri savrulma kuvveti
    public float invincibilityDuration = 1f; // �arpma sonras� dokunulmazl�k s�resi
    private bool isInvincible = false; // oyuncu ge�ici olarak zarar g�rmez

    public Color damageColor = Color.red; // hasar al�nca k�rm�z� yap
    private Color originalColor; // orijinal rengi kaydet
    public float colorChangeDuration = 0.2f; // k�rm�z� kalma s�resi

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
        // D��man�n EnemyInfo bile�enini al
        EnemyInfo enemyInfo = collision.gameObject.GetComponent<EnemyInfo>();

        // E�er d��manla �arp���yorsak ve d��man �lmemi�se hasar al
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
        currentHealth -= damage; // can� azalt
        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0) // can s�f�rdan k���kse �l
        {
            Die();
        }
        else
        {
            Vector2 knockbackDirection = (transform.position - enemyTransform.position); // normalize edilmi� y�n
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse); // kuvveti uygula

            // oyuncuyu dokunulmaz yap ve rengini de�i�tir
            StartCoroutine(InvincibilityCoroutine());
            StartCoroutine(ChangeColorCoroutine());
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; // oyuncuyu dokunulmaz yap
        yield return new WaitForSeconds(invincibilityDuration); // belirlenen s�re kadar bekle
        isInvincible = false; // tekrar hasar alabilir
    }

    IEnumerator ChangeColorCoroutine()
    {
        spriteRenderer.color = damageColor; // k�rm�z� yap
        yield return new WaitForSeconds(colorChangeDuration); // belirli bir s�re k�rm�z� kals�n
        spriteRenderer.color = originalColor; // eski renge geri d�n
    }

    void Die() // �lme
    {
        Debug.Log("Player Died!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
