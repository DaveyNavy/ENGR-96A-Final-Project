using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots;

    protected override void Start()
    {
        base.Start();
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.InventorySystem;
        }
    }
    public void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }
}