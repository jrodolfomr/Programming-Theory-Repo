using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuHandler : MonoBehaviour
{
    public InputField NameText;
    public Text BestScoreText;
    public void LoadMenuScene()
    {
        if (NameText.text != "")
        {

            GameManager.Instance.Name = NameText.text;
            SceneManager.LoadScene(1);
        }
    }
    public void ResetHighScoreHandler()
    {
        GameManager.Instance.ResetHiScore();
        BestScoreText.text = $"Best Score : [noname] : 0";
    }
    public void Start()
    {
        GameManager.Instance.LoadHiScore(out int hiscore, out string name);
        if (hiscore > 0)
            BestScoreText.text = $"Best Score : {name}: {hiscore}";

    }
    public void Exit()
    {
   
    #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
    #else
                Application.Quit(); // original code to quit Unity player
    #endif
    }
}
