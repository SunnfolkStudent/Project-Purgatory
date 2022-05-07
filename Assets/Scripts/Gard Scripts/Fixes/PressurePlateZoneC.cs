using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateZoneC : MonoBehaviour
{
   

    [SerializeField] private Animator _Animator;

    [HideInInspector] public bool pressureplateActivatedHold;

    [SerializeField] private GameObject Flame1;
    [SerializeField] private GameObject Flame2;

    private bool flame1Active;
    private bool flame2Acrive;
    
    

    private void Start()
    {
        Flame1.SetActive(false);
        Flame2.SetActive(false);
    }

    private void Update()
    {
        if (pressureplateActivatedHold)
        {
            Flame1.SetActive(true);
            flame1Active = true;
        }
        else if (!pressureplateActivatedHold)
        {
            Flame1.SetActive(false);
            flame1Active = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box" || other.gameObject.tag == "Player")
        {
            pressureplateActivatedHold = true;
            _Animator.Play("PressurePlateC");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box" || other.gameObject.tag == "Player")
        {
            _Animator.Play("PressureplateUpC");
            pressureplateActivatedHold = false;
        }
    }
}
