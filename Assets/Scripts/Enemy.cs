using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 5;
    float speed = 0.003f;
    int aggroRange = 2;
    int damageDealt = 2;

    SpriteRenderer spriteRenderer;
    Animator enemyAnim;
    [SerializeField] GameObject player;
    [SerializeField] bool bigSlime;
    Rigidbody2D rb;
    List<RaycastHit2D> hits = new List<RaycastHit2D>();
    public ContactFilter2D contactFilter;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (bigSlime)
        {
            health = 50;
            damageDealt = 5;
            aggroRange = 1;
            speed = 0.0025f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            enemyAnim.SetTrigger("die");
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < aggroRange)
        {
            moveToPlayer();
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }
 
    private void moveToPlayer()
    {
        
        Vector3 movementVector = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0).normalized;
        int count = rb.Cast(movementVector, contactFilter, hits, speed * movementVector.magnitude);
        if (count == 0  || bigSlime) { transform.position += speed * movementVector; }

        if (movementVector.x > 0)
        {
            spriteRenderer.flipX = true;
        } else if (movementVector.x < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void die()
    {
        Destroy(gameObject);
    }

    public int getDamage()
    {
        return damageDealt;
    }
}