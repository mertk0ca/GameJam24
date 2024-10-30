using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;                 // Oyuncunun transform bile�eni
    public float moveSpeed = 2.0f;           // D��man�n temel hareket h�z�
    public float chaseRange = 5.0f;          // D��man�n oyuncuyu takip etmeye ba�layaca�� mesafe
    public float smoothTime = 0.3f;          // D��man�n hedefe yumu�ak ge�i� s�resi (damping etkisi)
    public Animator animator;

    private Vector2 velocity = Vector2.zero; // Hareket h�z�n� izlemek i�in gerekli (SmoothDamp i�in)
    private Vector3 initialScale;             // Golemin ba�lang�� �l�e�i
    private EnemyInfo enemyInfo;              // D��man�n EnemyInfo bile�eni

    public AudioClip golemTriggerSound;
    private AudioSource golemTriggerSource;

    private bool hasPlayedSound = false;      // Sesin �al�n�p �al�nmad���n� kontrol eden de�i�ken

    void Start()
    {
        // Oyuncunun nesnesini etiket �zerinden bulabiliriz (Etiketin "Player" oldu�undan emin olun)
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Golemin ba�lang�� �l�e�ini kaydet
        initialScale = transform.localScale;

        // EnemyInfo bile�enini al
        enemyInfo = GetComponent<EnemyInfo>();

        golemTriggerSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // D��man �ld�yse hareket etmeyi durdur
        if (enemyInfo != null && enemyInfo.isDead)
        {
            animator.SetBool("isRunning", false); // Ko�ma animasyonunu durdur
            return; // Daha fazla i�lem yapma
        }

        // Oyuncu ile d��man aras�ndaki mesafeyi hesapla
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // E�er oyuncu belirli bir mesafedeyse, yaln�zca x ekseninde ona do�ru hareket et
        if (distanceToPlayer <= chaseRange)
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            animator.SetBool("isRunning", true);

            // Golemin y�n�n� ayarlamak i�in
            if (targetPosition.x > transform.position.x)
            {
                // Golem sa�a bak�yor
                transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z); // Sa�
            }
            else
            {
                // Golem sola bak�yor
                transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z); // Sol
            }

            // Sesin yaln�zca bir kez �al�nmas�n� sa�la
            if (!hasPlayedSound)
            {
                golemTriggerSource.clip = golemTriggerSound;
                golemTriggerSource.Play();
                Debug.Log("asdasd");
                hasPlayedSound = true; // Sesin �al�nd���n� i�aretle
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            hasPlayedSound = false; // Mesafeden ��k�ld���nda sesi tekrar �almak i�in s�f�rla
        }
    }
}
