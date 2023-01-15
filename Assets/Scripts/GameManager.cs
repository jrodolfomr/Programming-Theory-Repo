using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
  
    public static GameManager Instance { get; private set; }
    public bool isPlaying = false;
    public string Name; // new variable declared
    [System.Serializable]
    class HighScoreData
    {
        public string Name;
        public int HiScore;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
    //ABSTRACTION


    public void SaveHiScore(int HiScore)
    {
        if(Name== "")
        {
            Name = "testname";
        }
        HighScoreData data = new HighScoreData
        {
            Name = Name,
            HiScore = HiScore
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void ResetHiScore()
    {
        HighScoreData data = new HighScoreData
        {
            Name = "Name",
            HiScore = 0
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public bool LoadHiScore(out int HiScore, out string Name)
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);

            Name = data.Name;
            HiScore = data.HiScore;
            return true;
        }
        HiScore = 0;
        Name = "";
        return false;
    }

}
