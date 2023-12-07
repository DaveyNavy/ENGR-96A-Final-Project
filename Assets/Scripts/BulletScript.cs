using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{
    int speed = 5;
    int damage = 3;
    Rigidbody2D rb;
    Vector2 initialPosition;
    [SerializeField] private float range = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)((offset - transform.position));
        direction.Normalize();
        rb.velocity = direction * speed;
        initialPosition = rb.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }

            SkullWolf wolf = collision.GetComponent<SkullWolf>();
            if (wolf != null)
            {   
                wolf.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        if ((rb.position - initialPosition).magnitude > range)
        {
            Destroy(gameObject);
        }
    }
}