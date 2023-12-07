using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickScript : MonoBehaviour
{
    public float kickRadius = 0.07f;
    PlayerController playerController;
    int damage = 1;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (playerController.kickOn)
        {
            CheckKickOverlap();
        }
    }

    void CheckKickOverlap()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, kickRadius);
        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            SkullWolf wolf = hitCollider.GetComponent<SkullWolf>();
            if (wolf != null)
            {   
                wolf.TakeDamage(damage);
            }
        }
        playerController.kickOn = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, kickRadius);
    }
}

