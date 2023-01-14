using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AerealEnemy : Enemy
{
    //ENCAPSULATION
    private int m_height;
    public int Height { get { return m_height; } set { if (value < 0) { m_height = 0; } else { m_height = value; } } }

    private void Start()
    {
        Transform t = gameObject.transform;
        Height = Random.Range(3, 6);
        gameObject.transform.position = new Vector3(t.position.x, Height, t.position.z);

        if(t.position.z < 0)
        {
            gameObject.transform.rotation *= Quaternion.Euler(new Vector3(0f, -180f, 0f));
        }
        StartCoroutine(TextSwitcher());
    }
    public override void TakeDamage(int healthDamage) //POLYMORPHISM
    {
        Health -= healthDamage * 1;
        base.TakeDamage(healthDamage);

    }
    public override void TakeDamage() // ABSTRACTION
    {
        Health -= Random.Range(1, 2);
        base.TakeDamage();
    }
}
