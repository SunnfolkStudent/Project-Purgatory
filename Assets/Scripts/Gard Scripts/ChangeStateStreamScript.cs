using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeStateStreamScript : MonoBehaviour
{
    private bool isIce;
    private bool isWater;

    private bool isIceStream;
    private bool isWaterStream;

    public GameObject IceVisualTile;

    [SerializeField] private PhysicsMaterial2D iceMaterial;
    [SerializeField] private PhysicsMaterial2D defultMaterial;

    public bool canFreezeObject; 

    private void Start()
    {
        isIce = false;
        isWater = false;
        isIceStream = false;
        isWaterStream = false;
        canFreezeObject = false;
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

        
        if (isWaterStream)
        {
            print("changing");
            gameObject.tag = "WaterStreamIce";
        }
        else if (isIceStream)
        {
            gameObject.tag = "WaterStream";
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
        }

        if (gameObject.tag == "Ice")
        {
            isIce = true;
            isWater = false;
            int iceLayer = LayerMask.NameToLayer("Ice");
            gameObject.layer = iceLayer;
            gameObject.GetComponent<CompositeCollider2D>().isTrigger = false;
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
