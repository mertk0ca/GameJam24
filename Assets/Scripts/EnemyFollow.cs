using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;                 // Oyuncunun transform bileþeni
    public float moveSpeed = 2.0f;           // Düþmanýn temel hareket hýzý
    public float chaseRange = 5.0f;          // Düþmanýn oyuncuyu takip etmeye baþlayacaðý mesafe
    public float smoothTime = 0.3f;          // Düþmanýn hedefe yumuþak geçiþ süresi (damping etkisi)
    public Animator animator;

    private Vector2 velocity = Vector2.zero; // Hareket hýzýný izlemek için gerekli (SmoothDamp için)
    private Vector3 initialScale;             // Golemin baþlangýç ölçeði
    private EnemyInfo enemyInfo;              // Düþmanýn EnemyInfo bileþeni

    public AudioClip golemTriggerSound;
    private AudioSource golemTriggerSource;

    private bool hasPlayedSound = false;      // Sesin çalýnýp çalýnmadýðýný kontrol eden deðiþken

    void Start()
    {
        // Oyuncunun nesnesini etiket üzerinden bulabiliriz (Etiketin "Player" olduðundan emin olun)
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Golemin baþlangýç ölçeðini kaydet
        initialScale = transform.localScale;

        // EnemyInfo bileþenini al
        enemyInfo = GetComponent<EnemyInfo>();

        golemTriggerSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Düþman öldüyse hareket etmeyi durdur
        if (enemyInfo != null && enemyInfo.isDead)
        {
            animator.SetBool("isRunning", false); // Koþma animasyonunu durdur
            return; // Daha fazla iþlem yapma
        }

        // Oyuncu ile düþman arasýndaki mesafeyi hesapla
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Eðer oyuncu belirli bir mesafedeyse, yalnýzca x ekseninde ona doðru hareket et
        if (distanceToPlayer <= chaseRange)
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            animator.SetBool("isRunning", true);

            // Golemin yönünü ayarlamak için
            if (targetPosition.x > transform.position.x)
            {
                // Golem saða bakýyor
                transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z); // Sað
            }
            else
            {
                // Golem sola bakýyor
                transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z); // Sol
            }

            // Sesin yalnýzca bir kez çalýnmasýný saðla
            if (!hasPlayedSound)
            {
                golemTriggerSource.clip = golemTriggerSound;
                golemTriggerSource.Play();
                Debug.Log("asdasd");
                hasPlayedSound = true; // Sesin çalýndýðýný iþaretle
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            hasPlayedSound = false; // Mesafeden çýkýldýðýnda sesi tekrar çalmak için sýfýrla
        }
    }
}
