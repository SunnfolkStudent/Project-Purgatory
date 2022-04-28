using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPushing : MonoBehaviour
{


    private bool isOnIce;
    [SerializeField] private float raycastRange = 2f;
    [SerializeField] private LayerMask Icelayer;
    [SerializeField] private Rigidbody2D _Rigidbody2D;

    private Vector2 startPosition;
    
    void Start()
    {
        isOnIce = false;
        startPosition = _Rigidbody2D.position;
    }

    
    void Update()
    {
        LockMovement();
    }

    private void LockMovement()
    {
        var hitice = Physics2D.Raycast(transform.position, Vector2.down, raycastRange, Icelayer);

        if (hitice.collider != null)
        {
            isOnIce = true;
        }
        else
        {
            isOnIce = false;
        }

        if (isOnIce)
        {
            _Rigidbody2D.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            _Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ResetZone")
        {
            print("reset");
            gameObject.transform.position = startPosition;
        }
    }
}
