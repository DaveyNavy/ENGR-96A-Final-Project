using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthUp", menuName = "Collectables/HealthUp", order = 0)]

public class Health : Collectable
{
    public int healAmount;

    public override void OnCollect()
    {
        base.OnCollect();
        Debug.Log($"Healed for {healAmount} health.\n");

        PlayerController.Instance.RestoreHealth(healAmount);
    }
}
