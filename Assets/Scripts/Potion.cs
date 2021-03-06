using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{

    public float lifetime = 1f;
    public int damage = 1;
    public GameObject explosionPrefab;
    public Rigidbody2D potionRB;
    [SerializeField] private float explosionLifetime = 2f;

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
            explode(false);
            Destroy(gameObject);
        }
    }

    void explode(bool contact)
    {
        GameObject explosion = Instantiate(explosionPrefab, potionRB.transform.position, potionRB.transform.rotation);
        explosion.GetComponent<Animator>().SetBool("Contact", contact);
        Destroy(explosion, explosionLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            explode(true);
            if (collision.gameObject.GetComponent<EnemyHealth>()) collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            Debug.Log("hit");
            Destroy(gameObject);
        }
    }
}
