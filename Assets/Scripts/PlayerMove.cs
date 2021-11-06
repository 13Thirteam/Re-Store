using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    //Vector2 movement;
    float dirX;
    float dirY;
   
    // Update is called once per frame

    public bool speeding = false;
    private float speedTimer = 0.0f;

    void Update()
    {
        // speeding up
        if (speeding)
        {
            moveSpeed = 20f;
            speedTimer += Time.deltaTime;
            Debug.Log("speeding: " + speedTimer);
            if (speedTimer >= 2.0f)
            {
                speeding = false;
                moveSpeed = 5.0f;
            }
        }

        dirX = Input.GetAxis("Horizontal") * moveSpeed;
        dirY = Input.GetAxis("Vertical") * moveSpeed;

    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, dirY);
    }
}
