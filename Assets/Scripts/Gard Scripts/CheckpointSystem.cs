using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    
    [HideInInspector] public Vector2 playerStartPosition;
    [HideInInspector] public Vector2 playerCurrentPosition;
    
    public Rigidbody2D _Rigidbody2D;

    public GameObject Player;
    
    void Start()
    {
        playerStartPosition = _Rigidbody2D.position;
        playerCurrentPosition = playerStartPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ResetZone")
        {
            //gameObject.transform.position = playerCurrentPosition;
            Player.gameObject.transform.position = playerCurrentPosition;
        }

        if (other.gameObject.tag == "Checkpoint")
        {
            //playerCurrentPosition = gameObject.transform.position;
            playerCurrentPosition = Player.gameObject.transform.position;
        }
    }
}