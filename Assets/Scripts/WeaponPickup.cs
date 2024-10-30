using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private PlayerShooting playerShooting;

    private void Start()
    {
        playerShooting = FindObjectOfType<PlayerShooting>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShooting playerShooting = collision.GetComponent<PlayerShooting>();

            if (playerShooting != null)
            {
                playerShooting.GetComponent<PlayerShooting>().hasWeapon = true;
                playerShooting.ammo += 10;
            }
            Destroy(gameObject);
        }
    }
}
