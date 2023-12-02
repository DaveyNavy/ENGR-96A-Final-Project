using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] Collectable itemData;
    [SerializeField] int stackSize;

    public Collectable ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(Collectable source, int amt)
    {
        itemData = source;
        stackSize = amt;
    }

    public InventorySlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public bool RoomLeftInStack(int amtToAdd, out int amtRemaining)
    {
        amtRemaining = ItemData.maxStackSize - amtToAdd;
        return RoomLeftInStack(amtToAdd);
    }

    public bool RoomLeftInStack(int amtToAdd)
    {
        if (stackSize + amtToAdd <= itemData.maxStackSize)
        {
            return true;
        }
        return false;
    }
    public void AddToStack(int amt)
    {
        stackSize += amt;
    }

    public void RemoveFromStack(int amt)
    {
        stackSize -= amt;
    }

    public void UpdateInventorySlot(Collectable data, int amt)
    {
        itemData = data;
        stackSize = amt;
    }
}