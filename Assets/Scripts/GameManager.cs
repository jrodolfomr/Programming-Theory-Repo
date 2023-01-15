using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
  
    public static GameManager Instance { get; private set; } 
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
    void Start()
    {
    }
    //ABSTRACTION
   
   
}
