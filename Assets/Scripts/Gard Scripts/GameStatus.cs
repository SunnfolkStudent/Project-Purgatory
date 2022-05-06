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

    [SerializeField] private GameObject DoorToZoneA;
    [SerializeField] private GameObject DoorToZoneB;
    
    [SerializeField] private GameObject DoorToZoneAClosed;
    [SerializeField] private GameObject DoorToZoneBClosed;

    [SerializeField] private GameObject DoorToZoneC;

    [SerializeField] private GameObject CompletedZoneAlight;
    [SerializeField] private GameObject CompletedZoneBlight;
    
    void Start()
    {
        
        DoorToZoneA.SetActive(true);
        DoorToZoneB.SetActive(true);
        
        DoorToZoneAClosed.SetActive(false);
        DoorToZoneBClosed.SetActive(false);
        
        DoorToZoneC.SetActive(false);
        
        CompletedZoneAlight.SetActive(false);
        CompletedZoneBlight.SetActive(false);
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
            CompletedZoneAlight.SetActive(true);
        }

        if (HasCompletedZoneB)
        {
            DoorToZoneB.SetActive(false);
            DoorToZoneBClosed.SetActive(true);
            CompletedZoneBlight.SetActive(true);
        }

        if (CanAccessZoneC)
        {
            DoorToZoneC.SetActive(true);
        }
    }
}
