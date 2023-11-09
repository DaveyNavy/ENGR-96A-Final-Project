using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 5;
    SpriteRenderer spriteRenderer;
    Animator enemyAnim;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            enemyAnim.SetTrigger("die");
            Destroy(gameObject);
        }
        else
        {
            transform.position += new Vector3(0.005f * Mathf.Sin(Time.time), 0, 0);
            if (Mathf.Sin(Time.time) < 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (Mathf.Sin(Time.time) > 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }
}