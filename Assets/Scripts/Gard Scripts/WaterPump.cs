using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Input = Assets.Scripts.General_Scripts.Input;

public class WaterPump : MonoBehaviour
{
    [SerializeField] private GameObject WaterStream1;
    [SerializeField] private GameObject WaterStreamIce1;
    
    [SerializeField] private GameObject WaterStream2;
    [SerializeField] private GameObject WaterStreamIce2;

    private bool canTurnOnNr1;
    private bool canTurnOnNr2;

    private bool canInteract;

    public Input _Input;

    [HideInInspector] public bool canflip;
    [SerializeField] private Animator _Animator;

    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private AudioClip LeverSFX;

    private void Start()
    {
        WaterStream1.gameObject.SetActive(false);
        WaterStreamIce1.gameObject.SetActive(false);
        WaterStream2.gameObject.SetActive(false);
        WaterStreamIce2.gameObject.SetActive(false);
        
        canTurnOnNr1 = false;
        canTurnOnNr2 = false;
        canInteract = false;
    }

    private void Update()
    {
        SwitchPumpStreams();


        if (canflip && _Input.Interact)
        {
            _Animator.Play("LightLever");
            _AudioSource.PlayOneShot(LeverSFX);
        }

    }
    
    
    private void SwitchPumpStreams()
    {
        if (canTurnOnNr1 && !canTurnOnNr2 && _Input.Interact && canInteract) // 1stage
        {
            WaterStream1.gameObject.SetActive(true);
            WaterStream1.gameObject.tag = "WaterStream";
            
            canTurnOnNr1 = false;
            canTurnOnNr2 = true;
        }
        else if (!canTurnOnNr1 && canTurnOnNr2 && _Input.Interact && canInteract) //2stage
        {
            WaterStream1.gameObject.SetActive(false);
            WaterStream2.gameObject.SetActive(true);
            WaterStream2.gameObject.tag = "WaterStream";
            
            canTurnOnNr1 = false;
            canTurnOnNr2 = false;
        }
        else if (!canTurnOnNr1 && !canTurnOnNr2 && _Input.Interact && canInteract) //3stage
        {
            WaterStream2.gameObject.SetActive(false);
            
            canTurnOnNr1 = true;
            canTurnOnNr2 = false;
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "InteractRange")
        {
            canflip = true; // animation
            canInteract = true;

            if (!WaterStream1.gameObject.activeInHierarchy && !WaterStream2.gameObject.activeInHierarchy)
            {
                canTurnOnNr1 = true;
                canTurnOnNr2 = false;
            }
            else if (WaterStream1.gameObject.activeInHierarchy && !WaterStream2.gameObject.activeInHierarchy)
            {
                canTurnOnNr1 = false;
                canTurnOnNr2 = true;
            }
            else if (!WaterStream1.gameObject.activeInHierarchy && WaterStream2.gameObject.activeInHierarchy)
            {
                canTurnOnNr1 = false;
                canTurnOnNr2 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "InteractRange")
        {
            canTurnOnNr1 = false;
            canTurnOnNr2 = false;
            canflip = false;
            canInteract = false;
        } 
        
        
    }
}
