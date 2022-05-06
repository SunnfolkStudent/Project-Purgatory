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
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (HasCompletedZoneA && HasCompletedZoneB)
        {
            CanAccessZoneC = true;
        } 
    }
}
