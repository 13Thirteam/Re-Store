using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float detectLength;
    [SerializeField] private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        player = GameController.player;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        Vector2 moveDir = player.position - transform.position;
        RaycastHit2D hit1 = Physics2D.Raycast(
           transform.position,
           moveDir.normalized,
           detectLength,
           layerMask
        );
        if(hit1.collider != null)
        {
            hit1.collider.gameObject.GetComponent<PlayerHealth>().Kill();
        }
    }
}
