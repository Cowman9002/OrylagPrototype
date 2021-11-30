using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string mainScene;

    public void OnButton()
    {
        SceneManager.LoadScene(mainScene);
    }
}
