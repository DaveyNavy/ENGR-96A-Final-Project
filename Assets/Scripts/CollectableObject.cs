using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    [SerializeField] private Collectable collectable;
    [SerializeField] private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer.sprite = collectable.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") 
        {
            collectable.OnCollect();
            Destroy(gameObject);
        }
    }
}
