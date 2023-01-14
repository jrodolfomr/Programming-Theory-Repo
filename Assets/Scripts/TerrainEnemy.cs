
// INHERITANCE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TerrainEnemy : Enemy
{
    //ENCAPSULATION
    private int m_Steps;
    public int Steps  { get {
            return m_Steps;
        } set { 
            if(value < 0){
                m_Steps = 0;
            }
            else
            {
                m_Steps = value;
            }
        } 
    }
    Vector3 newPosition;
 
    private  void Start()
    {
        Transform t = gameObject.transform;
        gameObject.transform.position = new Vector3(t.position.x,1f, Mathf.Abs(t.position.z));
        StartCoroutine(TextSwitcher());
    }

    public override void TakeDamage(int healthDamage) //POLYMORPHISM
    {
       
        Health -= healthDamage*3;
     
        base.TakeDamage(healthDamage);
    }
    public override void TakeDamage() // ABSTRACTION
    {
        Health -= Random.Range(1, 4);
        base.TakeDamage();
    }
}
