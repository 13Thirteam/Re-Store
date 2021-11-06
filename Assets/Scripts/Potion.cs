using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{

    public float lifetime = 1f;
    public int damage = 1;
    public GameObject explosionPrefab;
    public Rigidbody2D potionRB;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (potionRB.velocity.magnitude < lifetime)
        {
            explode();
            Destroy(gameObject);
        }
    }

    void explode()
    {
        GameObject potion = Instantiate(explosionPrefab, potionRB.transform.position, potionRB.transform.rotation);
        Destroy(potion, 2f);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Enemy")
        {
            explode();
            target.transform.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }
    }
}
