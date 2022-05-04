using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStreamPushing : MonoBehaviour
{

    public Rigidbody2D box_RigidBody2D;

    [HideInInspector] public bool canPush;

    [SerializeField] private float pushForce = 1f;
    


    private void Update()
    {
        if (canPush)
        {
            box_RigidBody2D.AddForce(transform.up * pushForce, ForceMode2D.Force);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box")
        {
            canPush = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box")
        {
            canPush = false;
        }
    }
}
