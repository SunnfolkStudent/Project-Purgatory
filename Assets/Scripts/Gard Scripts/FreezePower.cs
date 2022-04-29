using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input = Assets.Scripts.General_Scripts.Input;

public class FreezePower : MonoBehaviour
{

    public Input _Input;

    [HideInInspector] public ChangeStateScript FreezableObject;
    
   
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

        if (other.gameObject.tag == "WaterStream" || other.gameObject.tag == "WaterStreamIce")
        {
            FreezableObject = other.gameObject.GetComponent<ChangeStateScript>();
        }
        
    }
    
}
