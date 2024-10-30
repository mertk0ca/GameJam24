using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // mermi prefabi
    public Transform firePoint; // merminin firlatilacagi yer
    public float bulletSpeed = 10f; // mermi hizi
    public bool hasWeapon = false;
    public int ammo = 0;

    private float cooldownDuration = 0.5f; // cooldown suresi
    private bool isCooldown = false; // cooldown durumu

    public AudioClip throwSound;
    private AudioSource throwSource;

    private void Start()
    {
        throwSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && hasWeapon) // silah varsa
        {
            if (!isCooldown) // cooldownda degilse
            {
                if (ammo > 0)
                {
                    Shoot();
                    throwSource.clip = throwSound;
                    throwSource.volume = 1f;
                    throwSource.Play();
                    ammo -= 1;
                    StartCoroutine(ShootCooldown()); // cooldown coroutineini baslat
                    Debug.Log("Kalan Mermi: " + ammo);
                }
                else
                {
                    Debug.Log("Mermi Bitti!");
                }
            }
        }
    }
    private IEnumerator ShootCooldown()
    {
        isCooldown = true; // cooldown durumunu true yap
        yield return new WaitForSeconds(cooldownDuration); // belirtilen sure boyunca bekle
        isCooldown = false; // cooldown durumunu false yap
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed; // sag yone ates et
    }
}