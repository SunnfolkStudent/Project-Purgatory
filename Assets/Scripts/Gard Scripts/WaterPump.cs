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
    private bool canTurnOffNr1;
    private bool canTurnOnNr2;
    private bool canTurnOffNr2;

    public Input _Input;

    [HideInInspector] public bool canflip;
    [SerializeField] private Animator _Animator;

    private void Start()
    {
        WaterStream1.gameObject.SetActive(false);
        WaterStreamIce1.gameObject.SetActive(false);
        WaterStream2.gameObject.SetActive(false);
        WaterStreamIce2.gameObject.SetActive(false);
        
        canTurnOnNr1 = false;
        canTurnOnNr2 = false;
        canTurnOffNr1 = false;
        canTurnOffNr2 = false;
    }

    private void Update()
    {
        SwitchPumpStreams();


        if (canflip && _Input.Interact)
        {
            _Animator.Play("LightLever");
        }


        if (canTurnOnNr1)
        {
            print("can turn on nr1");
        }

        if (canTurnOnNr2)
        {
            print("can turn on nr2");
        }
        
        //if (canTurnOffNr1)
        {
            print("can turn off nr1");
        }
        
        //if (canTurnOffNr2)
        {
            print("can turn off nr2");
        }
    }
    
    
    private void SwitchPumpStreams()
    {
        if (canTurnOnNr1 && !canTurnOnNr2 && !canTurnOffNr1 && !canTurnOffNr2 && _Input.Interact) // 1stage
        {
            WaterStream1.gameObject.SetActive(true);
            WaterStream1.gameObject.tag = "WaterStream";
            
            canTurnOnNr1 = false;
            canTurnOnNr2 = true;
            canTurnOffNr1 = true;
            canTurnOffNr2 = false;
        }
        else if (!canTurnOnNr1 && canTurnOnNr2 && canTurnOffNr1 && !canTurnOffNr2 && _Input.Interact) //2stage
        {
            WaterStream1.gameObject.SetActive(false);
            WaterStream2.gameObject.SetActive(true);
            WaterStream2.gameObject.tag = "WaterStream";
            
            canTurnOnNr1 = false;
            canTurnOnNr2 = false;
            canTurnOffNr1 = false;
            canTurnOffNr2 = true;
        }
        else if (!canTurnOnNr1 && !canTurnOnNr2 && !canTurnOffNr1 && canTurnOffNr2 && _Input.Interact) //3stage
        {
            WaterStream2.gameObject.SetActive(false);
            
            
            canTurnOnNr1 = true;
            canTurnOnNr2 = false;
            canTurnOffNr1 = false;
            canTurnOffNr2 = false;
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "InteractRange")
        {
            canflip = true;

            if (!WaterStream1.gameObject.activeInHierarchy && !WaterStream2.gameObject.activeInHierarchy)
            {
                canTurnOnNr1 = true;
                canTurnOnNr2 = false;
                canTurnOffNr1 = false;
                canTurnOffNr2 = false;
            }
            else if (WaterStream1.gameObject.activeInHierarchy && !WaterStream2.gameObject.activeInHierarchy)
            {
                canTurnOnNr1 = false;
                canTurnOnNr2 = true;
                canTurnOffNr1 = true;
                canTurnOffNr2 = false;
            }
            else if (!WaterStream1.gameObject.activeInHierarchy && WaterStream2.gameObject.activeInHierarchy)
            {
                canTurnOnNr1 = false;
                canTurnOnNr2 = false;
                canTurnOffNr1 = false;
                canTurnOffNr2 = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "InteractRange")
        {
            canTurnOnNr1 = false;
            canTurnOffNr1 = false;
            canTurnOnNr2 = false;
            canTurnOffNr1 = false;
            canflip = false;
        } 
        
        
    }
}
