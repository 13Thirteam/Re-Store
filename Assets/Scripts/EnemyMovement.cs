using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float detectLength;
    [SerializeField] private LayerMask layerMask;
    private Transform player;
    [SerializeField] private bool obstacleBlocked = false;
    private Vector2 dodgeDir;
    private Animator animator;
    public bool attacking = false;
    private string currentDir;
    private Vector2 animDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GameController.player;
        animator = GetComponent<Animator>();
        StartCoroutine(animDelay());
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    public void EnemyMove()
    {
        Vector3 moveVec = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        Vector2 moveDir = player.position - transform.position;
        RaycastHit2D hit1 = Physics2D.Raycast(
           (Vector2)transform.position + Vector2.Perpendicular(moveDir).normalized * transform.localScale * .5f,
           moveDir.normalized,
           detectLength,
           layerMask
        ); 
        RaycastHit2D hit2 = Physics2D.Raycast(
           (Vector2)transform.position - Vector2.Perpendicular(moveDir).normalized * transform.localScale * .5f,
            moveDir.normalized,
            detectLength,
            layerMask
         );
        Debug.DrawRay((Vector2)transform.position + Vector2.Perpendicular(moveDir).normalized * transform.localScale * .5f,
           moveDir.normalized,
           Color.white,
           Time.deltaTime);

        Debug.DrawRay((Vector2)transform.position - Vector2.Perpendicular(moveDir).normalized * transform.localScale * .5f,
            moveDir.normalized,
           Color.white,
           Time.deltaTime);
        if (hit1.collider != null || hit2.collider != null)
        {
            if(!obstacleBlocked)
            {
                obstacleBlocked = true;
                dodgeDir = Vector2.Perpendicular(moveDir);
            }
            if (!attacking) {
                transform.position = (Vector2)transform.position + dodgeDir / moveDir.magnitude * moveSpeed * Time.deltaTime;
                animDir = moveDir;
            }
        }
        else
        {
            if (!attacking) {
                animDir = moveVec - transform.position;
                transform.position = moveVec;
            }
        }
    }

    private IEnumerator animDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            MoveAnimate(animDir);
        }
    }

    private void MoveAnimate(Vector2 dir)
    {
        if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0 && currentDir != "R")
            {
                animator.SetTrigger("MoveRight");
                currentDir = "R";
            }
            else if (dir.x < 0 && currentDir != "L")
            {
                animator.SetTrigger("MoveLeft");
                currentDir = "L";
            }
        }
        else
        {
            if (dir.y > 0 && currentDir != "U")
            {
                animator.SetTrigger("MoveUp");
                currentDir = "U";
            }
            else if (dir.y < 0 && currentDir != "D")
            {
                animator.SetTrigger("MoveDown");
                currentDir = "D";
            }
        }
    }
}
