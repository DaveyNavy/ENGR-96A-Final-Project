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
    [SerializeField] private List<Collectable> loot;
    [SerializeField] private GameObject collectablePrefab;
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
            enemyAnim.SetTrigger("Die");
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < aggroRange)
        {
            MoveToPlayer();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
 
    private void MoveToPlayer()
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

    void Die()
    {
        DropLoot();
        Destroy(gameObject);
    }

    public int GetDamage()
    {
        return damageDealt;
    }

    void DropLoot()
    {
        Debug.Log("Dropping Loot.\n");
        foreach (Collectable c in loot) 
        {
            CollectableObject collectableObject = Instantiate(collectablePrefab, transform.position, transform.rotation).GetComponent<CollectableObject>();
            collectableObject.SetCollectable(c);
        }
    }
}