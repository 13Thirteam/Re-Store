using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Transform player;
    private Animator animator;
    [SerializeField] private float detectLength;
    [SerializeField] private LayerMask layerMask;
    private EnemyMovement move;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<EnemyMovement>();
        player = GameController.player;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (!move.obstacleBlocked)
        {
            Vector2 moveDir = player.position - transform.position;
            RaycastHit2D hit1 = Physics2D.Raycast(
               transform.position,
               moveDir.normalized,
               detectLength,
               layerMask
            );
            if (hit1.collider != null)
            {
                hit1.collider.gameObject.GetComponent<PlayerHealth>().Kill();
                GetComponent<EnemyMovement>().attacking = true;
                animator.SetTrigger("Attack");
            }
        }
    }
}
