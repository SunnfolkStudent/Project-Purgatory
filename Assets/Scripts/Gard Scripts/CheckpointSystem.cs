using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input = Assets.Scripts.General_Scripts.Input;

public class CheckpointSystem : MonoBehaviour
{
    
    [HideInInspector] public Vector2 playerStartPosition;
    [HideInInspector] public Vector2 playerCurrentPosition;
    
    public Rigidbody2D _Rigidbody2D;
    [SerializeField] Input _Input;

    public GameObject Player;


    [SerializeField] private AudioClip PlayerDeathSFX;
    [SerializeField] private AudioSource _AudioSource;
    
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
            _AudioSource.PlayOneShot(PlayerDeathSFX);
            _Rigidbody2D.velocity = Vector2.zero;
            Player.gameObject.transform.position = playerCurrentPosition;
        }

        if (other.gameObject.tag == "Checkpoint")
        {
            //playerCurrentPosition = gameObject.transform.position;
            playerCurrentPosition = Player.gameObject.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "FreezePowerPickup")
        {
            print("in triggerbox");
            if (_Input.Interact)
            {
                GameStatus.HasPickedUpFreezePower = true;
                print("Player in zone");
            }
        }
    }
}
