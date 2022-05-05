using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour
{

    public PressurePlate _PressurePlateBox;
    public PressurePlatePlayer _PressurePlatePlayer;

    [SerializeField] private Animator door;
    //[SerializeField] private Animator door2;
    //[SerializeField] private Animator door3;
    //[SerializeField] private Animator door4;

    [HideInInspector] public bool doorUnlocked;
    

    void Update()
    {
        if (_PressurePlatePlayer.pressureplateActivatedToggled)
        {
            //gameObject.SetActive(false);
            door.Play("FirstDoor");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            print("played animation");

        }

        if (_PressurePlateBox.pressureplateActivatedHold)
        {
            door.Play("FirstDoor");
            //gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            print("played animation");
        }
        else
        {
            //gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
