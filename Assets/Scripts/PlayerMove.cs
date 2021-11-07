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

    private Animator animator;
    private string currentDir;

    // Update is called once per frame

    public bool speeding = false;
    private float speedTimer = 0.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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
        MoveAnimate(new Vector2(dirX, dirY));
    }

    private void MoveAnimate(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0 && currentDir != "R")
            {
                animator.SetBool("MoveLeft", false);
                animator.SetBool("MoveRight", true);
                animator.SetBool("MoveUp", false);
                animator.SetBool("MoveDown", false);
                currentDir = "R";
            }
            else if (dir.x < 0 && currentDir != "L")
            {
                animator.SetBool("MoveLeft", true);
                animator.SetBool("MoveRight", false);
                animator.SetBool("MoveUp", false);
                animator.SetBool("MoveDown", false);
                currentDir = "L";
            }
        }
        else
        {
            if (dir.y > 0 && currentDir != "U")
            {
                animator.SetBool("MoveLeft", false);
                animator.SetBool("MoveRight", false);
                animator.SetBool("MoveUp", true);
                animator.SetBool("MoveDown", false);
                currentDir = "U";
            }
            else if (dir.y < 0 && currentDir != "D")
            {
                animator.SetBool("MoveLeft", false);
                animator.SetBool("MoveRight", false);
                animator.SetBool("MoveUp", false);
                animator.SetBool("MoveDown", true);
                currentDir = "D";
            }
        }
        if(dir.magnitude<0.1f)
        {
            currentDir = "I";
            animator.SetBool("MoveLeft", false);
            animator.SetBool("MoveRight", false);
            animator.SetBool("MoveUp", false);
            animator.SetBool("MoveDown", false);
            animator.SetTrigger("Idle");
        }
    }
}
