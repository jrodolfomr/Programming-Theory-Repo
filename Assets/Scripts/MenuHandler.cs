using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{

    public void LoadMenuScene()
    {
        GameManager.Instance.Name = "boyo";
         
        SceneManager.LoadScene(1);
    }
}
