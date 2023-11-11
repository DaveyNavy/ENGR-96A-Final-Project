using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GourmetIngredient", menuName = "Collectables/GourmetIngredient", order = 0)]
public class GourmetIngredient : Collectable
{
    public int newScene;
    public override void OnCollect() {
        base.OnCollect();

        Debug.Log($"Collected gourmet ingredient. Changing scene to {newScene}.\n");

        SceneManager.LoadScene(newScene);
    }
}
