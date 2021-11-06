using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health;
    
    CorpseManager corpseManager;

    //TEMP
    [SerializeField] bool button = false;
    bool currentValue = false;

    private void Start()
    {
        corpseManager = FindObjectOfType<CorpseManager>();
    }

    private void Update()
    {
        /*if(button != currentValue) //button to trigger death in editor
        {
            currentValue = button;
            Die();
        }*/
    }

    public void TakeDamage(int damage) //deals damage to enemy
    {
        health -= damage;
        if (health <= 0) Die();
    }

    private void Die() 
    {
        corpseManager.makeCorpse(transform.position);
        GameController.killCount++;
        Destroy(gameObject);
    }
}
