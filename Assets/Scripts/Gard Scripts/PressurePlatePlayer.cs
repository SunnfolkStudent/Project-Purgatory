using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlatePlayer : MonoBehaviour
{

    [HideInInspector] public bool pressureplateActivatedToggled;
    
    [SerializeField] private Animator Animation;
    [SerializeField] private string PressurePlateAnimations;

    private void Start()
    {
        pressureplateActivatedToggled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            pressureplateActivatedToggled = true;
            //Animation.Play(PressurePlateAnimations);
            print("pressed");
        }
    }
    
}
