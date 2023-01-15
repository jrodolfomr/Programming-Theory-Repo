using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    private string m_name;
    private int m_health;
    private int m_max_health;
    private int m_attack;
    private int m_defense;

    private float m_speed;

    //ENCAPSULATION 
    public string Name { get { return m_name; } set { if (value == null) { m_name = ""; } else { m_name = value; } } }
    public int Health { get { return m_health; } set { if (value < 0) { m_health = 0; } else { m_health = value; SetText(); } } }

    public int MaxHealth { get { return m_max_health; } set { if (value < 0) { m_max_health = 0; } else { m_max_health = value; } } }
    public int Attack { get { return m_attack; } set { if (value < 0) { m_attack = 0; } else { m_attack = value; } } }
    public int Defense { get { return m_defense; } set { if (value < 0) { m_defense = 0; } else { m_defense = value; } } }

    public float Speed { get { return m_speed; } set { if (value < 0) { m_speed = 1; } else { m_speed = value; } } }

    public string[] EnemyNames;

    private GameObject internalText;
    private TextMesh healthText;

    public Spawner spawner;


    private void Awake()
    {
        internalText = transform.Find("Text").gameObject;
        healthText = internalText.GetComponent<TextMesh>();
        if(EnemyNames.Length >0)
        Name = EnemyNames[Random.Range(0, EnemyNames.Length)];
     
        Attack = Random.Range(1, 10);
        Defense = Random.Range(1, 10);
    }
    public void SetText()
    {
        healthText.text = $"{Name}\nHP: {Health}/{MaxHealth}";
    }
    private void SetTextDestroyed()
    {
        healthText.text = $"{Name}\nDestroyed";
    }
    private void Update()
    {
        
        Transform internalTransform = internalText.transform;

        var playerPos = Camera.main.transform.position;
        playerPos.y = internalTransform.transform.position.y;
        //  transform.LookAt(playerPos);

        internalTransform.transform.LookAt(playerPos);
        internalTransform.transform.Rotate(new Vector3(0, 180f, 0f));
        //   internalTransform.rotation = Quaternion.Euler(0f, internalTransform.rotation.y,internalTransform.rotation.z);
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    public virtual void TakeDamage(int healthDamage) // POLYMORPHISM
    {
        Debug.Log($"Normal: {gameObject.name}=>{Health}");
        StopAllCoroutines();
      

        if (Health <= 0)
        {
            StartCoroutine(TextDestroy());
        }
        else
        {
            StartCoroutine(TextSwitcher());
        }
    }
    public virtual void TakeDamage() // POLYMORPHISM
    {
        Debug.Log($"Random: {gameObject.name}=>{Health}");
        StopAllCoroutines();
      

        if (Health <= 0)
        {
            StartCoroutine(TextDestroy());
        }
        else
        {
            StartCoroutine(TextSwitcher());
        }
    }

    private IEnumerator TextDestroy()
    {
        SetTextDestroyed();
        yield return new WaitForSeconds(1); // wait
        healthText.text = "";
        Destroy(gameObject);
       spawner.CheckEnemyCount();
        spawner.CalculatePoints(Attack);
    }

    public IEnumerator TextSwitcher()
    {
        SetText();
        yield return new WaitForSeconds(2); // wait
        healthText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collider"))
        {
            Debug.Log("Triggered by Collider");
            spawner.SetGameOver();
        }
        if (other.CompareTag("OutOfBound"))
        {
            Debug.Log("Triggered by OutOfBound");
            StartCoroutine(TextDestroy());
        }
    }
}

