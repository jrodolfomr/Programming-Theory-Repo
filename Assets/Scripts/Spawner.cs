using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update

    public float EnemySpeedRate = 1f;
    public List<Enemy> enemies = new List<Enemy>();
    public int enemyCount = 0;
    void Start()
    {
        spawnEnemies();
    }
    public void spawnEnemies()
    {
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
            g.Speed = EnemySpeedRate;
            g.spawner = this;
        }

    }
    public void CheckEnemyCount()
    {
        enemyCount -= 1;
        if (enemyCount == 0)
        {
           EnemySpeedRate *= 1.25f;
           spawnEnemies();

        }

    }
}
