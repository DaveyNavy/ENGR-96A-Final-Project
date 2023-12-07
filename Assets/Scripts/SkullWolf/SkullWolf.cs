using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SkullWolf : MonoBehaviour
{

    public float health = 5;
    float speed = 1f;
    int aggroRange = 2;
    int damageDealt = 2;
    

    SpriteRenderer spriteRenderer;
    Animator enemyAnim;
    [SerializeField] GameObject player;
    [SerializeField] private List<Collectable> loot;
    [SerializeField] private GameObject collectablePrefab;
    Rigidbody2D rb;
    List<RaycastHit2D> hits = new List<RaycastHit2D>();
    public ContactFilter2D contactFilter;
    

    //
    // public IcicleSpawner icicleSpawner;
    // bool specialAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        health = 25;
        damageDealt = 5;
        aggroRange = 1;
        speed = 0.0025f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0){
            StartCoroutine(WaitForDeathAnimation());
        }       
        else if (Vector2.Distance(transform.position, player.transform.position) < aggroRange){
            MoveToPlayer();
        }
        // else if (health <= 7 && specialAttack){
        //     Debug.Log("its here");
        //     icicleSpawner.SpawnIcicles();
        //     specialAttack = false;
        // }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Took Damage.\n");
        Debug.Log(health);
    }

    private void MoveToPlayer()
    {
        Vector3 movementVector = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0).normalized;
        int count = rb.Cast(movementVector, contactFilter, hits, speed * movementVector.magnitude);
        
        if (count == 0){
            transform.position += speed * movementVector;
            enemyAnim.SetBool("isRunning", true); // Set isRunning to true when moving
        }
        else{
            enemyAnim.SetBool("isRunning", false); // Set isRunning to false when not moving
        }

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

    IEnumerator WaitForDeathAnimation()
    {
    // Trigger the death animation
    enemyAnim.SetTrigger("Die");

    // Wait for the length of the animation
    yield return new WaitForSeconds(enemyAnim.GetCurrentAnimatorStateInfo(0).length);

    // Call Die method after the animation
    Die();
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
