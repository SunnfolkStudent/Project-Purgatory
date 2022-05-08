using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{


    [SerializeField] public static bool HasPickedUpFreezePower;
    [SerializeField] public static bool HasPickedUpLightPower;
    [SerializeField] public static bool HasCompletedZoneA;
    [SerializeField] public static bool HasCompletedZoneB;
    [SerializeField] public static bool CanAccessZoneC;
    [SerializeField] public static bool CanPickUpFinalSoul;

    [SerializeField] private GameObject DoorToZoneA;
    [SerializeField] private GameObject DoorToZoneB;
    
    [SerializeField] private GameObject DoorToZoneAClosed;
    [SerializeField] private GameObject DoorToZoneBClosed;

    [SerializeField] private GameObject DoorToZoneC;

    [SerializeField] private GameObject LeftBackground;
    [SerializeField] private GameObject RightBackground;
    [SerializeField] private GameObject DefultBackground;
    [SerializeField] private GameObject OpenBackground;
    
    
    void Start()
    {
        
        DoorToZoneA.SetActive(true);
        DoorToZoneB.SetActive(true);
        
        DoorToZoneAClosed.SetActive(false);
        DoorToZoneBClosed.SetActive(false);
        
        DoorToZoneC.SetActive(false);
        
        LeftBackground.SetActive(false);
        RightBackground.SetActive(false);
        OpenBackground.SetActive(false);
        DefultBackground.SetActive(true);
    }
    
    
    void Update()
    {
        if (HasCompletedZoneA && HasCompletedZoneB)
        {
            CanAccessZoneC = true;
        }

        if (HasCompletedZoneA)
        {
            DoorToZoneA.SetActive(false);
            DoorToZoneAClosed.SetActive(true);
            LeftBackground.SetActive(true);
        }

        if (HasCompletedZoneB)
        {
            DoorToZoneB.SetActive(false);
            DoorToZoneBClosed.SetActive(true);
            RightBackground.SetActive(true);
        }

        if (CanAccessZoneC)
        {
            DoorToZoneC.SetActive(true);
            OpenBackground.SetActive(true);
        }
    }
}
