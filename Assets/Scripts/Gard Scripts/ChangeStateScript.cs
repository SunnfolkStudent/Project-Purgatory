using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeStateScript : MonoBehaviour
{

    public FreezePower _FreezePower;
    public Tilemap _WaterIceTimap;

    [HideInInspector] private bool isIce;
    [HideInInspector] private bool isWater;

    public GameObject IceVisualTile;
    
    private void Start()
    {
        isIce = false;
        isWater = false;
    }

    void Update()
    {
        StateStatus();
    }


    public void ChangeState()
    {
        print("frrrrreeeeezzzeee");
        
        if (isWater)
        {
            gameObject.tag = "Ice";
        }
        else if (isIce)
        {
            gameObject.tag = "Water";
        }
    }
    
    private void StateStatus()
    {
        if (gameObject.tag == "Water")
        {
            isWater = true;
            isIce = false;
            int waterLayer = LayerMask.NameToLayer("Water");
            gameObject.layer = waterLayer;
            gameObject.GetComponent<CompositeCollider2D>().isTrigger = true;
            IceVisualTile.SetActive(false);
            print("water");
        }

        if (gameObject.tag == "Ice")
        {
            isIce = true;
            isWater = false;
            int iceLayer = LayerMask.NameToLayer("Ice");
            gameObject.layer = iceLayer;
            gameObject.GetComponent<CompositeCollider2D>().isTrigger = false;
            IceVisualTile.SetActive(true);
            print("ice");
        }
    }
}
