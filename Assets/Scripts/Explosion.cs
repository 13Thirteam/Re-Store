using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnColliderEnter2D(Collider2D target)
    {
        if (target.tag == "Enemy")
        {
            target.transform.SendMessage("TakeDamage", damage);
            Debug.Log("hit");
        }
    }
}
