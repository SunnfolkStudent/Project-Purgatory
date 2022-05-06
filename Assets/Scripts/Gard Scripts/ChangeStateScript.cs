using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeStateScript : MonoBehaviour
{
    private bool isIce;
    private bool isWater;

    private bool isIceStream;
    private bool isWaterStream;

    public GameObject IceVisualTile;
    public WaterStreamPushing _WaterStreamPushing = null;

    [SerializeField] private PhysicsMaterial2D iceMaterial;
    [SerializeField] private PhysicsMaterial2D defultMaterial;
    [SerializeField] private CompositeCollider2D WaterCompositeCollider2D = null;
    [SerializeField] private GameObject waterStreamVisual;

    public bool canFreezeObject;

    [SerializeField] private AudioSource _AudioSource;
    [SerializeField] private AudioClip FreezeSFX;
    [SerializeField] private AudioClip UnFreezeSFX;
    [SerializeField] private AudioClip WaterStreamSFX;

    private void Start()
    {
        isIce = false;
        isWater = false;
        isIceStream = false;
        isWaterStream = false;
        canFreezeObject = false;

        _AudioSource.volume = 0.5f;


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
            _AudioSource.PlayOneShot(FreezeSFX);
        }
        else if (isIce)
        {
            gameObject.tag = "Water";
            _AudioSource.PlayOneShot(UnFreezeSFX);
        }

        
        if (isWaterStream && !_WaterStreamPushing.canPush)
        {
            gameObject.tag = "WaterStreamIce";
            _AudioSource.PlayOneShot(FreezeSFX);
        }
        else if (isIceStream)
        {
            gameObject.tag = "WaterStream";
            _AudioSource.PlayOneShot(UnFreezeSFX);
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
            WaterCompositeCollider2D.sharedMaterial = defultMaterial;
            IceVisualTile.SetActive(false);
        }

        if (gameObject.tag == "Ice")
        {
            isIce = true;
            isWater = false;
            int iceLayer = LayerMask.NameToLayer("Ice");
            gameObject.layer = iceLayer;
            gameObject.GetComponent<CompositeCollider2D>().isTrigger = false;
            WaterCompositeCollider2D.sharedMaterial = iceMaterial;
            IceVisualTile.SetActive(true);
        }

        if (gameObject.tag == "WaterStream")
        {
            isWaterStream = true;
            isIceStream = false;
            int waterLayer = LayerMask.NameToLayer("Water");
            gameObject.layer = waterLayer;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<BoxCollider2D>().sharedMaterial = defultMaterial;
            IceVisualTile.SetActive(false);
            waterStreamVisual.SetActive(true); 

        }

        if (gameObject.tag == "WaterStreamIce")
        {
            isIceStream = true;
            isWaterStream = false;
            int iceLayer = LayerMask.NameToLayer("Ice");
            gameObject.layer = iceLayer;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            gameObject.GetComponent<BoxCollider2D>().sharedMaterial = iceMaterial;
            IceVisualTile.SetActive(true);
            waterStreamVisual.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "FreezePowerHitbox")  // need to make it false but wont work for some reason
        {
            canFreezeObject = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "FreezePowerHitbox")
        {
            canFreezeObject = false;
        }
    }
}
