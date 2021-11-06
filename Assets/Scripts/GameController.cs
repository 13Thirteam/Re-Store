using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Transform player;
    [SerializeField] private Transform playerRef;
    // Start is called before the first frame update
    void Awake()
    {
        player = playerRef;
        //player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
