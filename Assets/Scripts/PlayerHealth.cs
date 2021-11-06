using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerMove playerMove;
    private Rigidbody2D rb;
    [SerializeField] private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill()
    {
        playerMove.enabled = false;
        rb.velocity = new Vector2(0, 0);
        gameController.Die();
    }
}
