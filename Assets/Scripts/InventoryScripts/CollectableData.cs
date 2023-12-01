using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData : ScriptableObject
{
    public int ID;
    [TextArea(4, 4)]
    public string Name;
    public string Description;
    public Sprite icon;
    
}
