using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    int health = 100;
    public TextMeshProUGUI healthText;
    float iFramesStart = 0;

    public float speed = 0;
    private Vector2 movement;

    Rigidbody2D rb;
    List<RaycastHit2D> hits = new List<RaycastHit2D>();
    public ContactFilter2D contactFilter;

    Animator playerAnim;
    SpriteRenderer spriteRenderer;

    private float fireCooldownStart = -3;
    int ammo = 10;
    public TextMeshProUGUI ammoText;
    [SerializeField] GameObject bullet;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(2);
        }

        SetAmmoText();
        SetHealthText();
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
        } else if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerStay2D (Collider2D collider)
    {
        if (collider.CompareTag("Enemy") && Time.time  - iFramesStart > 0.5f)
        {
            if (collider.GetComponent<Enemy>())
            {
                Enemy enemy = (Enemy) collider.GetComponent<Enemy>();
                takeDamage(enemy.getDamage());
                iFramesStart = Time.time;
            }           
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 movementVector = value.Get<Vector2>().normalized;
        movement = new Vector2(movementVector.x, movementVector.y);
    }

    void OnFire(InputValue value)
    {

        if (Time.time - fireCooldownStart > 1 && ammo > 0)
        {
            playerAnim.SetTrigger("fire");
            Instantiate(bullet, transform.position, transform.rotation);
            fireCooldownStart = Time.time;
            ammo--;
        }
    }

    void SetAmmoText()
    {
        ammoText.text = "Ammo: " + ammo.ToString();
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    void takeDamage(int damage)
    {
        health -= damage;
    }
}