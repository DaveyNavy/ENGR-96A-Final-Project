using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoReload", menuName = "Collectables/AmmoReload", order = 0)]

public class Ammo : Collectable
{
    public int reloadAmount;

    public override void OnCollect()
    {
        base.OnCollect();
        Debug.Log($"Collected {reloadAmount} more bullets.\n");

        PlayerController.Instance.ReloadAmmo(reloadAmount);
    }
}
