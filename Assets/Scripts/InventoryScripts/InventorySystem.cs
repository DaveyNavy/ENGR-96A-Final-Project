using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;
    public List<InventorySlot> InventorySlots => inventorySlots;

    public int inventorySize => InventorySlots.Count;
    public UnityAction <InventorySlot> OnInventorySlotChanged;

    public InventorySystem (int size)
    {
        inventorySlots = new List<InventorySlot> (size);
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory (Collectable itemToAdd, int amt)
    {
        if (ContainsItem(itemToAdd, out InventorySlot invSlot))
        {
            if (invSlot.RoomLeftInStack(amt))
            {
                invSlot.AddToStack(amt);
                OnInventorySlotChanged?.Invoke(invSlot);
                return true;
            }
        } 
        else if (HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amt);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
        return false;
    }

    public bool ContainsItem(Collectable itemToAdd, out InventorySlot invSlot) 
    {
        invSlot = null;
        foreach (InventorySlot i in inventorySlots)
        {
            if (i.ItemData != null && i.ItemData.equals(itemToAdd))
            {
                invSlot = i;
                break;
            }
        }
        return invSlot != null;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = null;
        foreach (InventorySlot i in inventorySlots) {
            if (i.ItemData == null)
            {
                freeSlot = i;
                break;
            }
        }
        return freeSlot != null;
    }
}