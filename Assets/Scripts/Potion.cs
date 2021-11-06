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
        /*foreach(Explosion explosion in FindObjectsOfType<Explosion>())
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), explosion.GetComponent<Collider2D>(), true);
        }*/
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            explode();
            if (collision.gameObject.GetComponent<EnemyHealth>()) collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            Debug.Log("hit");
            Destroy(gameObject);
        }
    }
}
