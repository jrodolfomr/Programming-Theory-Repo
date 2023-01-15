using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update

    public float EnemySpeedRate = 1.25f;
    public List<Enemy> enemies = new List<Enemy>();
    public int enemyCount = 0;
    public float EnemySpeed = 1f;
    private int hiscore;
    public Text ScoreText;
    public Text BestScoreText;
    public Text LevelText;
    public GameObject resetButton;
    public GameObject backButton;
    private string text = "";
    private int m_Points;
    private int m_level;
    void Start()
    {
        if (GameManager.Instance != null)
        {
            text = GameManager.Instance.Name;

            GameManager.Instance.LoadHiScore(out hiscore, out string name);
            if (hiscore > 0)
                BestScoreText.text = $"Best Score : {name}: {hiscore}";
            else
                BestScoreText.text = "";
        }
        GameManager.Instance.isPlaying = true;

         LevelText.text = "Level 1";
         spawnEnemies();
    }
    public void spawnEnemies()
    {
        m_level += 1;
        LevelText.text = $"Level {m_level}";
        for (var i = 0; i < 10; i++)
        {
            int selected = Random.Range(0, 3);
            float temp_x = Random.Range(-10f, 10f);
            float temp_y = Random.Range(-10f, 10f);
            float temp_z = Random.Range(-10f, 10f);
            Enemy g = Instantiate(enemies[selected], new Vector3(temp_x, temp_y, temp_z), Quaternion.Euler(0, 0, 0));

            enemyCount += 1;
            // g.Name = "Enemy";
            g.Health = Random.Range(1, 10);
            g.MaxHealth = g.Health;
            g.Attack = Random.Range(1, 10);
            g.Defense = Random.Range(1, 10);
            g.Speed = EnemySpeed;
            g.spawner = this;
        }

    }
    public void SetGameOver()
    {
        GameManager.Instance.isPlaying = false;
        resetButton.SetActive(true);
        backButton.SetActive(true);
        if (GameManager.Instance != null)
        {
            if (m_Points > hiscore)
            GameManager.Instance.SaveHiScore(m_Points);
        }

        
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void CheckEnemyCount()
    {
        enemyCount -= 1;
        if (enemyCount == 0)
        {
           EnemySpeed *= EnemySpeedRate;
           spawnEnemies();

        }

    }
    public void CalculatePoints(int points)
    {
        m_Points += points;
        ScoreText.text = $"{text} => Score : {m_Points}";
    }
}
