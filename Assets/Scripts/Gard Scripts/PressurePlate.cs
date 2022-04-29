using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool canPressdown;

    [HideInInspector] public bool pressureplateActivatedHold;


    private void Update()
    {
        if (canPressdown)
        {
            pressureplateActivatedHold = true;
        }
        else
        {
            pressureplateActivatedHold = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box" || other.gameObject.tag == "Player")
        {
            canPressdown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box" || other.gameObject.tag == "Player")
        {
            canPressdown = false;
        }
    }
}
