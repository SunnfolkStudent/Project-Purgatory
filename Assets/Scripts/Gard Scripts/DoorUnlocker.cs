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
    [SerializeField] private string DoorCloseAnimations;

    private bool Open;
    

    void Update()
    {
        if (_PressurePlatePlayer.pressureplateActivatedToggled)
        {
            DoorAnimation.Play(DoorAnimations);
        }

        if (_PressurePlateBox.pressureplateActivatedHold)
        {
            
            DoorAnimation.Play(DoorAnimations);
            Open = true;
        }
        else if (!_PressurePlateBox.pressureplateActivatedHold && Open)
        { 
            DoorAnimation.Play(DoorCloseAnimations);
            Open = false;
        }
    }
}
