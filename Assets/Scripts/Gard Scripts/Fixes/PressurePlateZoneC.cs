using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateZoneC : MonoBehaviour
{
   

    [SerializeField] private Animator _Animator;

    [HideInInspector] public bool pressureplateActivatedHold;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box" || other.gameObject.tag == "Player")
        {
            pressureplateActivatedHold = true;
            _Animator.Play("PressurePlateC");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box" || other.gameObject.tag == "Player")
        {
            _Animator.Play("PressureplateUpC");
            pressureplateActivatedHold = false;
        }
    }
}
