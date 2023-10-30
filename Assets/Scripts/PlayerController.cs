using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;

    private float movementX = 0;
    private float movementY = 0;
    [SerializeField] GameObject bullet;
    void Start()
    {
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * (new Vector3(movementX, movementY, 0));
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>().normalized;

        movementX = v.x;
        movementY = v.y;
    }

    void OnFire(InputValue value)
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}