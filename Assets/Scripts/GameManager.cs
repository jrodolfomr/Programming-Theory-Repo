using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    public Camera GameCamera;
    public float PanSpeed = 10.0f;

    public  List<Enemy> enemies = new List<Enemy>();

    void Start()
    {

        for(var i= 0; i <10; i++)
        {
            int selected = Random.Range(0, 3);
            float temp_x = Random.Range(-10f, 10f);
            float temp_y = Random.Range(-10f, 10f);
            float temp_z = Random.Range(-10f, 10f);
            Enemy g = Instantiate(enemies[selected], new Vector3(temp_x, temp_y, temp_z), Quaternion.Euler(0, 0, 0));
           // g.Name = "Enemy";
            g.Health = Random.Range(1, 10);
            g.MaxHealth = g.Health;
            g.Attack = Random.Range(1, 10);
            g.Defense = Random.Range(1, 10);
         
        }

      
        
    }
    //ABSTRACTION
    private bool SelectEnemy(out Enemy e)
    {
        e = null;
        RaycastHit t;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out t))
        {
            var enemy = t.collider.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
               
                e = enemy;
                return true;

            }
        }
        return false;
    }

    private void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        GameCamera.transform.position = GameCamera.transform.position + PanSpeed * Time.deltaTime * new Vector3(move.y, 0, -move.x);
        if (Input.GetMouseButtonDown(0))
        {
            if (SelectEnemy(out Enemy enemy))
                if(enemy.Health>0)
                  enemy.TakeDamage(1);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (SelectEnemy(out Enemy enemy))
                 if (enemy.Health > 0)
                    enemy.SetText();
        }
    }
}
