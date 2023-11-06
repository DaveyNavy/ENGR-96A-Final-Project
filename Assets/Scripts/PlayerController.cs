using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;

    private Vector2 movement;

    Rigidbody2D rb;
    List<RaycastHit2D> hits = new List<RaycastHit2D>();
    public ContactFilter2D contactFilter;
    Animator playerAnim;
    SpriteRenderer spriteRenderer;

    [SerializeField] GameObject bullet;
    void Start()
    {
        transform.position = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        int count = rb.Cast(movement, contactFilter, hits, speed * Time.deltaTime);
        if (count == 0 && movement != Vector2.zero) 
        {
            transform.position += speed * Time.deltaTime * new Vector3(movement.x, movement.y, 0) ;
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }

        if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        } else
        {
            spriteRenderer.flipX= false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue value)
    {
        Vector2 movementVector = value.Get<Vector2>().normalized;
        movement = new Vector2(movementVector.x, movementVector.y);
    }

    void OnFire(InputValue value)
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}