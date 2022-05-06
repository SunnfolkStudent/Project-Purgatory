using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlocker : MonoBehaviour
{

    public PressurePlate _PressurePlateBox;
    public PressurePlatePlayer _PressurePlatePlayer;

    [SerializeField] private Animator DoorAnimation;

    [HideInInspector] public bool doorUnlocked;

    [SerializeField] private string DoorAnimations;
    [SerializeField] private string DoorCloseAnimations;

    private bool Open;

    private AudioSource _AudioSource;
    [SerializeField] private AudioClip DoorSFX;

    private bool canPlayAudio;
    


    private void Start()
    {
        _AudioSource = gameObject.GetComponent<AudioSource>();
        canPlayAudio = true;
    }

    void Update()
    {
        if (_PressurePlatePlayer.pressureplateActivatedToggled)
        {
            if (canPlayAudio)
            {
                _AudioSource.PlayOneShot(DoorSFX);
                canPlayAudio = false;
            }
            DoorAnimation.Play(DoorAnimations);
        }

        if (_PressurePlateBox.pressureplateActivatedHold)
        {
            if (canPlayAudio)
            {
                _AudioSource.PlayOneShot(DoorSFX);
                canPlayAudio = false;
            }
            
            DoorAnimation.Play(DoorAnimations);
            Open = true;
        }
        else if (!_PressurePlateBox.pressureplateActivatedHold && Open)
        { 
            DoorAnimation.Play(DoorCloseAnimations);
            Open = false;
            canPlayAudio = true;
        }
    }
}
