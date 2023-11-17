using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
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
    int ammo = 0;
    public TextMeshProUGUI ammoText;
    [SerializeField] GameObject bullet;
    public bool kickOn = false;
    [SerializeField] private int maxHealth = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this);
        }

        else 
        {
            Instance = this;
        }
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
            rb.position += speed * Time.deltaTime * movement;
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }

        if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movement.x > 0)
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
                TakeDamage(enemy.GetDamage());
                iFramesStart = Time.time;
                AudioManager.instance.PlaySFX("Damage");
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

    void TakeDamage(int damage)
    {
        health -= damage;

    }

    void OnKick(InputValue value)
    {
        playerAnim.SetTrigger("kick");
        kickOn = true;
    }

    public void ReloadAmmo(int reloadAmount)
    {
        ammo += reloadAmount;
        SetAmmoText();
    }

    public void RestoreHealth(int restoreAmount)
    {
        if (health >= maxHealth)
        {
            Debug.Log("Health full already; did not heal.\n");
        }

        else if (health + restoreAmount > maxHealth)
        {
            health = maxHealth;
            Debug.Log("Health set to max.\n");
        }

        else 
        {
            health += restoreAmount;
        }
        
        SetHealthText();
    } 
}