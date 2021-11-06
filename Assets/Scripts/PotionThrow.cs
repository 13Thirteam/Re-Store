using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionThrow : MonoBehaviour
{

    public Transform firePoint;
    public GameObject potionPrefab;
    public float potionForce = 20f;
    public float fireRate = 10f;
    private float fireCooldown;

    // Start is called before the first frame update
    void Start()
    {
        fireCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && fireCooldown < 0)
        {
            fire();
            fireCooldown = fireRate;
        }
        fireCooldown -= Time.deltaTime;
    }

    void fire()
    {
        GameObject bullet = Instantiate(potionPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * potionForce, ForceMode2D.Impulse);
    }
}
