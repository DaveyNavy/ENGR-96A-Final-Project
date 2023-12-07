using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUp", menuName = "Collectables/SpeedUp", order = 0)]

public class SpeedMultipler : Collectable
{
    public float speedMultiplier;
    public float timeLimit;
    public override void OnCollect()
    {
        base.OnCollect();
    }

    public override void OnExecute()
    {
        Debug.Log($"Increased speed by {speedMultiplier}x.\n");

        PlayerController.Instance.SetSpeedMultiplier(speedMultiplier, timeLimit);
    }
}
