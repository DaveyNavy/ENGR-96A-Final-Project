using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    [SerializeField] private Collectable collectable;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Animator animator;
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        ReloadSprite();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var inventory = other.transform.GetComponent<InventoryHolder>();
            if (inventory.InventorySystem.AddToInventory(collectable, 1))
            {
                Destroy(gameObject);
            }
            collectable.OnCollect();
            Destroy(gameObject);
        }
    }

    public void SetCollectable(Collectable c)
    {
        collectable = c;
        ReloadSprite();
    }

    private void ReloadSprite()
    {
        spriteRenderer.sprite = collectable.sprite;

        if (collectable.animator == null) 
        {
            animator.runtimeAnimatorController = null;
        }

        else 
        {
            animator.runtimeAnimatorController = collectable.animator;
        }
    }
}
