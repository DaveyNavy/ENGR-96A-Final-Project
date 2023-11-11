using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collectable", menuName = "Collectables/BaseCollectable", order = 0)]
public class Collectable : ScriptableObject {
    public Sprite sprite;
    public new string name;
    public virtual void OnCollect() 
    {
        Debug.Log($"Collected {name}.\n");
    }
}