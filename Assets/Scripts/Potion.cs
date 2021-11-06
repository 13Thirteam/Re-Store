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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            explode();
            col.transform.SendMessage("TakeDamage", damage);
            Debug.Log("hit");
            Destroy(gameObject);
        }
    }
}
