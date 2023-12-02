using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "AmmoReload", menuName = "Collectables/AmmoReload", order = 0)]

public class Ammo : Collectable
{
    public int reloadAmount;

    public Ammo()
    {
        setType("Ammo");
    }
    public override void OnCollect()
    {
        AudioManager.instance.PlaySFX("Collect");
        base.OnCollect();
        Debug.Log($"Collected {reloadAmount} more bullets.\n");

        PlayerController.Instance.ReloadAmmo(reloadAmount);
    }
}