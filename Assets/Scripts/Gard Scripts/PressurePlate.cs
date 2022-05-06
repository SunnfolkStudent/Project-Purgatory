using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    //private bool canPressdown;

    [SerializeField] private Animator _Animator;

    [HideInInspector] public bool pressureplateActivatedHold;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box" || other.gameObject.tag == "Player")
        {
            //canPressdown = true;
            pressureplateActivatedHold = true;
            _Animator.Play("PressurePlate");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box" || other.gameObject.tag == "Player")
        {
            //canPressdown = false;
            _Animator.Play("PressureplateUp");
            pressureplateActivatedHold = false;
        }
    }
}
