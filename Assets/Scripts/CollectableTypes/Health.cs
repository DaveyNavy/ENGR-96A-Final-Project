using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthUp", menuName = "Collectables/HealthUp", order = 0)]

public class Health : Collectable
{
    public int healAmount;

    public override void OnCollect()
    {
        AudioManager.instance.PlaySFX("Collect");
        base.OnCollect();
    }

    public override void OnExecute()
    {
        Debug.Log($"Healed for {healAmount} health.\n");
        PlayerController.Instance.RestoreHealth(healAmount);
    }
}
