using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPump : MonoBehaviour
{
    [SerializeField] private GameObject WaterStream;
    [SerializeField] private GameObject WaterStreamIce;
    
    [SerializeField] private GameObject WaterStream1;
    [SerializeField] private GameObject WaterStreamIce1;
    
    

    private bool canFlipSwitch;
    private bool canUnflipSwitch;

    public Input _Input;

    private void Start()
    {
        WaterStream.gameObject.SetActive(false);
        WaterStreamIce.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (canFlipSwitch && _Input.Switch)
        {
            WaterStream.gameObject.SetActive(true);
            WaterStream.gameObject.tag = "WaterStream";
            canFlipSwitch = false;
            canUnflipSwitch = true;
        }
        else if (canUnflipSwitch && _Input.Switch)
        {
            WaterStream.gameObject.SetActive(false);
            canFlipSwitch = true;
            canUnflipSwitch = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "InteractRange")
        {
            if (WaterStream.gameObject.activeInHierarchy)
            {
                canFlipSwitch = false;
                canUnflipSwitch = true;
            }
            else if (!WaterStream.gameObject.activeInHierarchy)
            {
                canFlipSwitch = true;
                canUnflipSwitch = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "InteractRange")
        {
            canFlipSwitch = false;
            canUnflipSwitch = false;
        } 
    }
}
