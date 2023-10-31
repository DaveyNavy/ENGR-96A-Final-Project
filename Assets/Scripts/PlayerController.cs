using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;

    private float movementX = 0;
    private float movementY = 0;

    Rigidbody2D rb;
    List<RaycastHit2D> hits = new List<RaycastHit2D>();
    public ContactFilter2D contactFilter;

    [SerializeField] GameObject bullet;
    void Start()
    {
        transform.position = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();  
    }

    private void FixedUpdate()
    {
        int count = rb.Cast(new Vector2(movementX, movementY), contactFilter, hits, speed * Time.deltaTime);
        if (count == 0)
        {
            transform.position += speed * Time.deltaTime * (new Vector3(movementX, movementY, 0));
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue value)
    {
        Vector2 movementVector = value.Get<Vector2>().normalized;
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnFire(InputValue value)
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}