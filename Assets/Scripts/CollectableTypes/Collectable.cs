using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "Collectable", menuName = "Collectables/BaseCollectable", order = 0)]
public class Collectable : ScriptableObject
{
    public Sprite sprite;
    public new string name;
    public int maxStackSize = 999999;

    public RuntimeAnimatorController animator;
    public virtual void OnCollect()
    {
        AudioManager.instance.PlaySFX("Collect");
        Debug.Log($"Collected {name}.\n");
    }

    public virtual void OnExecute()
    {

    }

    public bool equals(Collectable c) {
        return name == c.name; 
    }
}