using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fartSpawn : MonoBehaviour
{
    public GameObject fartPrefab; // Fart objesi
    public Transform firePoint;    // Fart objesinin oluþturulacaðý nokta
    public float fartDuration = 3f; // Fart'ýn ne kadar süreyle üretileceði
    public float cooldownDuration = 5f; // Fart'ýn yeniden üretilebilmesi için beklenmesi gereken süre
    public float spawnDelay = 0.1f; // Her fart objesinin ne kadar sürede spawnlanacaðý

    public bool isFarting = false; // Fart iþlemi yapýlýp yapýlmadýðýný kontrol etmek için deðiþken
    public bool isCooldown = false; // Cooldown durumunu kontrol etmek için deðiþken

    public AudioClip gasSound;
    private AudioSource gasSource;

    private void Start()
    {
        gasSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Space tuþuna basýldýðýnda Fart iþlemini baþlat
        if (Input.GetKeyDown(KeyCode.Space) && !isFarting && !isCooldown)
        {
            gasSource.clip = gasSound;
            gasSource.Play();

            StartCoroutine(FartCoroutine());
        }
    }

    // Fart iþlemi için Coroutine
    IEnumerator FartCoroutine()
    {
        isFarting = true; // Fart iþlemini baþlat
        float endTime = Time.time + fartDuration; // Fart süresinin bitiþ zamaný

        while (Time.time < endTime) // Fart süresi boyunca
        {
            Fart(); // Fart oluþtur
            yield return new WaitForSeconds(spawnDelay); // Belirtilen süre kadar bekle
        }

        isFarting = false; // Fart iþlemini bitir
        isCooldown = true; // Cooldown durumunu aktif et
        yield return new WaitForSeconds(cooldownDuration); // Cooldown süresi boyunca bekle
        isCooldown = false; // Cooldown durumunu bitir
    }

    void Fart()
    {
        Instantiate(fartPrefab, firePoint.position, firePoint.rotation); // Fart objesini oluþtur
    }
}
