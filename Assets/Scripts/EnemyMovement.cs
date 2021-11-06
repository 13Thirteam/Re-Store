using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float detectLength;
    [SerializeField] private LayerMask layerMask;
    private Transform player;

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
        RaycastHit2D hit = Physics2D.Raycast(
           transform.position,
           moveVec.normalized,
           detectLength,
           layerMask
        );
        if (hit.collider != null)
        {
            transform.position = (Vector2)transform.position + Vector2.Perpendicular(transform.position - player.position)/moveVec.magnitude * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = moveVec;
        }
    }
}
