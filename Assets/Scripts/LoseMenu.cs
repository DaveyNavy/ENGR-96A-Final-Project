using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public void OnReturnButton()
    {
        SceneManager.LoadScene(0);
    }
}
