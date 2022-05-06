using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{


    public static bool HasPickedUpFreezePower;
    public static bool HasPickedUpLightPower;
    public static bool HasCompletedZoneA;
    public static bool HasCompletedZoneB;
    public static bool CanAccessZoneC;

    [SerializeField] private GameObject DoorToZoneA;
    [SerializeField] private GameObject DoorToZoneB;
    
    [SerializeField] private GameObject DoorToZoneAClosed;
    [SerializeField] private GameObject DoorToZoneBClosed;
    
    void Start()
    {
        
        DoorToZoneA.SetActive(true);
        DoorToZoneB.SetActive(true);
        
        DoorToZoneAClosed.SetActive(false);
        DoorToZoneBClosed.SetActive(false);

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
        }

        if (HasCompletedZoneB)
        {
            DoorToZoneB.SetActive(false);
            DoorToZoneBClosed.SetActive(true);
        }
    }
}
