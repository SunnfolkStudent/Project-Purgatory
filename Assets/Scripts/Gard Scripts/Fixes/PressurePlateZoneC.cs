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

    [SerializeField] private GameObject DoorClosed;
    [SerializeField] private GameObject DoorOpen;

    private bool flame1Active;
    private bool flame2Active;
    private bool DoorUnlocked;

    [SerializeField] private LightPowerSpot _LightPower;

    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private AudioClip DoorOpeningSFX;
    private bool canPLayAudio;

    private void Start()
    {
        Flame1.SetActive(false);
        Flame2.SetActive(false);
        canPLayAudio = true;
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

        if (_LightPower.FirstLightTriggerHit)
        {
            Flame2.SetActive(true);
            flame2Active = true;
        }
        else if (!_LightPower.FirstLightTriggerHit)
        {
           Flame2.SetActive(false);
           flame2Active = false;
        }

        if (flame1Active && flame2Active)
        {
            DoorUnlocked = true;
        }
        

        if (DoorUnlocked)
        {
            if (canPLayAudio)
            {
                _AudioSource.PlayOneShot(DoorOpeningSFX);
                canPLayAudio = false;
            }
            DoorOpen.SetActive(true);
            DoorClosed.SetActive(false);
        }
        else 
        {
            DoorClosed.SetActive(true); 
            DoorOpen.SetActive(false);
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
