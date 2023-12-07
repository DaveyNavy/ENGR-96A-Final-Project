using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    protected InventorySystem inventorySystem;

    public InventorySystem InventorySystem => inventorySystem;
    protected virtual void Start()
    {

    }

    public void SlotClicked(InventorySlot_UI clickedSlot)
    {
        Debug.Log("Slot clicked");
        clickedSlot.AssignedInventorySlot.ItemData.OnExecute();
        clickedSlot.AssignedInventorySlot.RemoveFromStack(1);
        clickedSlot.UpdateUISlot();
        if (clickedSlot.AssignedInventorySlot.StackSize <= 0)
        {
            clickedSlot.AssignedInventorySlot.ClearSlot();
            clickedSlot.ClearSlot();
        }
    }
}