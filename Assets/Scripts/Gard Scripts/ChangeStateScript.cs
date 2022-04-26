using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeStateScript : MonoBehaviour
{
    [HideInInspector] private bool isIce;
    [HideInInspector] private bool isWater;

    public GameObject IceVisualTile;

    [SerializeField] private PhysicsMaterial2D iceMaterial;
    [SerializeField] private PhysicsMaterial2D defultMaterial;
    [SerializeField] private CompositeCollider2D waterCollider;

    public bool canFreezeObject; 

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
            waterCollider.sharedMaterial = defultMaterial;
            IceVisualTile.SetActive(false);
        }

        if (gameObject.tag == "Ice")
        {
            isIce = true;
            isWater = false;
            int iceLayer = LayerMask.NameToLayer("Ice");
            gameObject.layer = iceLayer;
            gameObject.GetComponent<CompositeCollider2D>().isTrigger = false;
            waterCollider.sharedMaterial = iceMaterial;
            IceVisualTile.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "FreezePowerHitbox")
        {
            canFreezeObject = true;
        }
        else
        {
            canFreezeObject = false;
        }
    }
}
