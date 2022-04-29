using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool canPressdown;

    [HideInInspector] public bool pressureplateActivated;


    private void Update()
    {
        if (canPressdown)
        {
            pressureplateActivated = true;
        }
        else
        {
            pressureplateActivated = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box")
        {
            canPressdown = true;
            print("sadasndk");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box")
        {
            canPressdown = false;
        }
    }
}
