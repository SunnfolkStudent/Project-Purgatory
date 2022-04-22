using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePower : MonoBehaviour
{

    public Input _Input;

    private bool canFreeze;
    private bool canUnFreeze;

    public ChangeStateScript waterFreeze;
    
    
    void Start()
    {
        canFreeze = false;
        canUnFreeze = false;
    }

   
    void Update()
    {
        if ((canFreeze || canUnFreeze) && _Input.Magic)
        {
            waterFreeze.ChangeState();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water")
        {
            canFreeze = true;
        }
        else
        {
            canFreeze = false;
        }

        if (other.gameObject.tag == "Ice")
        {
            canUnFreeze = true;
        }
        else
        {
            canUnFreeze = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water" || other.gameObject.tag == "Ice")
        {
            waterFreeze = other.gameObject.GetComponent<ChangeStateScript>();
        }
        
    }
}
