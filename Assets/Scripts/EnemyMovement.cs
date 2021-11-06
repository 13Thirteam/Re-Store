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

    // Start is called before the first frame update
    void Start()
    {
        player = GameController.player;
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
            transform.position = (Vector2)transform.position + dodgeDir/moveDir.magnitude * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = moveVec;
        }
    }
}
