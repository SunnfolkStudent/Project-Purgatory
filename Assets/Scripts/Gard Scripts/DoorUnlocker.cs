using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour
{

    public PressurePlate _PressurePlateBox;
    public PressurePlatePlayer _PressurePlatePlayer;

    [SerializeField] private Animator DoorAnimation;
    //[SerializeField] private Animator door2;
    //[SerializeField] private Animator door3;
    //[SerializeField] private Animator door4;

    [HideInInspector] public bool doorUnlocked;

    [SerializeField] private string DoorAnimations;
    

    void Update()
    {
        if (_PressurePlatePlayer.pressureplateActivatedToggled)
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            DoorAnimation.Play(DoorAnimations);
            print("played animation");

        }

        if (_PressurePlateBox.pressureplateActivatedHold)
        {
            //door.Play("FirstDoor");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (!_PressurePlateBox.pressureplateActivatedHold && _PressurePlatePlayer.pressureplateActivatedToggled)
        { 
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
