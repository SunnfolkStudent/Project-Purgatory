using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateStreamScript : MonoBehaviour
{
    private bool isIce;
    private bool isWater;

    public GameObject IceVisualTile;

    [SerializeField] private PhysicsMaterial2D iceMaterial;
    [SerializeField] private PhysicsMaterial2D defultMaterial;

    [HideInInspector]public bool canFreezeObject; 

    private void Start()
    {
        isIce = false;
        isWater = false;
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
    }
    
    private void StateStatus()
    {
        if (gameObject.tag == "Water")
        {
            isWater = true;
            isIce = false;
            int waterLayer = LayerMask.NameToLayer("Water");
            gameObject.layer = waterLayer;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            IceVisualTile.SetActive(false);
        }

        if (gameObject.tag == "Ice")
        {
            isIce = true;
            isWater = false;
            int iceLayer = LayerMask.NameToLayer("Ice");
            gameObject.layer = iceLayer;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            IceVisualTile.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "FreezePowerHitbox") 
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