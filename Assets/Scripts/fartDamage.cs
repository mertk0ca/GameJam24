using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fartDamage : MonoBehaviour
{
    public float fartLifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, fartLifeTime); // Fart objesini belirli bir s�re sonra yok et
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
