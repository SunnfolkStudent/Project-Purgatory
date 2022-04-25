using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePower : MonoBehaviour // NEED TO FIX THE BUGWHERE IF THE HITBOX GOES OUSIDE THE ICE IT WONT TRIGGER ANYMORE
{

    public Input _Input;

    private bool canFreeze;
    private bool canUnFreeze;

    [HideInInspector]public ChangeStateScript FreezableObject;


    void Start()
    {
        canFreeze = false;
        canUnFreeze = false;
    }

   
    void Update()
    {
        if ((canFreeze || canUnFreeze) && _Input.Magic)
        {
            FreezableObject.ChangeState();
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water" || other.gameObject.tag == "Ice")
        {
            FreezableObject = other.gameObject.GetComponent<ChangeStateScript>();
        }
        
        //-----------------------------------------------//
        
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
        
        //----------------------------------------------//
    }
    
}
