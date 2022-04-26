using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePower : MonoBehaviour
{

    public Input _Input;

    [HideInInspector]public ChangeStateScript FreezableObject;
    
   
    void Update()
    {
        if (FreezableObject.canFreezeObject && _Input.Magic)
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
    }
    
}
