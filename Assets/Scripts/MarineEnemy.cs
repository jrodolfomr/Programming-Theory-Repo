
// INHERITANCE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MarineEnemy : Enemy
{
    //ENCAPSULATION
    private int WaterLevel { get; set; }

    private void Start()
    {
        Transform t = gameObject.transform;
        Debug.Log($"Marine T:{t.position}");
        gameObject.transform.position = new Vector3(t.position.x, 1f, -Mathf.Abs(t.position.z));
        gameObject.transform.rotation *= Quaternion.Euler(new Vector3(0f, -180f, 0f));
        StartCoroutine(TextSwitcher());
    }

    public override void TakeDamage(int healthDamage) //POLYMORPHISM
    {
        Health -= healthDamage * 2;
        base.TakeDamage(healthDamage);
    }
    public override void TakeDamage() // ABSTRACTION
    {
        Health -= Random.Range(1,3);
        base.TakeDamage();
    }

}

