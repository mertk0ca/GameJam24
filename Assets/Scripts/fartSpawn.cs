using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fartSpawn : MonoBehaviour
{
    public GameObject fartPrefab; // Fart objesi
    public Transform firePoint;    // Fart objesinin olu�turulaca�� nokta
    public float fartDuration = 3f; // Fart'�n ne kadar s�reyle �retilece�i
    public float cooldownDuration = 5f; // Fart'�n yeniden �retilebilmesi i�in beklenmesi gereken s�re
    public float spawnDelay = 0.1f; // Her fart objesinin ne kadar s�rede spawnlanaca��

    public bool isFarting = false; // Fart i�lemi yap�l�p yap�lmad���n� kontrol etmek i�in de�i�ken
    public bool isCooldown = false; // Cooldown durumunu kontrol etmek i�in de�i�ken

    public AudioClip gasSound;
    private AudioSource gasSource;

    private void Start()
    {
        gasSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Space tu�una bas�ld���nda Fart i�lemini ba�lat
        if (Input.GetKeyDown(KeyCode.Space) && !isFarting && !isCooldown)
        {
            gasSource.clip = gasSound;
            gasSource.Play();

            StartCoroutine(FartCoroutine());
        }
    }

    // Fart i�lemi i�in Coroutine
    IEnumerator FartCoroutine()
    {
        isFarting = true; // Fart i�lemini ba�lat
        float endTime = Time.time + fartDuration; // Fart s�resinin biti� zaman�

        while (Time.time < endTime) // Fart s�resi boyunca
        {
            Fart(); // Fart olu�tur
            yield return new WaitForSeconds(spawnDelay); // Belirtilen s�re kadar bekle
        }

        isFarting = false; // Fart i�lemini bitir
        isCooldown = true; // Cooldown durumunu aktif et
        yield return new WaitForSeconds(cooldownDuration); // Cooldown s�resi boyunca bekle
        isCooldown = false; // Cooldown durumunu bitir
    }

    void Fart()
    {
        Instantiate(fartPrefab, firePoint.position, firePoint.rotation); // Fart objesini olu�tur
    }
}
