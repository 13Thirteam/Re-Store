using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionThrow : MonoBehaviour
{

    public Transform firePoint;
    [SerializeField] float fireOffset;
    public GameObject potionPrefab;
    public float potionForce = 20f;
    public float fireRate = 0.1f;
    private float fireCooldown;

    public Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        fireCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && fireCooldown < 0)
        {
            fire(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && fireCooldown < 0)
        {
            fire(Vector2.down);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && fireCooldown < 0)
        {
            fire(Vector2.left);
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && fireCooldown < 0)
        {
            fire(Vector2.right);
        }
        fireCooldown -= Time.deltaTime;
        
    }

    void fire(Vector2 dir)
    {
        GameObject potion = Instantiate(potionPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = potion.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * potionForce, ForceMode2D.Impulse);
        fireCooldown = fireRate;

    }
}
