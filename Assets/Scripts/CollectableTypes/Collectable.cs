using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectable", menuName = "Collectables/BaseCollectable", order = 0)]
public class Collectable : ScriptableObject
{
    public Sprite sprite;
    public new string name;
    public string type;
    public int maxStackSize = 999999;
    public virtual void OnCollect()
    {
        AudioManager.instance.PlaySFX("Collect");
        Debug.Log($"Collected {name}.\n");
    }

    public void setType(string t)
    {
        type = t;
    }

    public bool equals(Collectable c) { return type == c.type; }
}