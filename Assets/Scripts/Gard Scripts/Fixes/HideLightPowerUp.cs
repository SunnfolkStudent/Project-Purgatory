using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLightPowerUp : MonoBehaviour
{
  
    [SerializeField] private PlayerMovement _PlayerMovement;
    
    void Update()
    {
        if (_PlayerMovement.HideLightPowerup)
        {
            gameObject.SetActive(false); 
        }
    }
}
