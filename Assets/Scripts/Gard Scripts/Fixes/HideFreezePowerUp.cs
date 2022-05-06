using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFreezePowerUp : MonoBehaviour
{

    [SerializeField] private PlayerMovement _PlayerMovement;
    
    void Update()
    {
        if (_PlayerMovement.HideFreezePowerUp)
        {
           gameObject.SetActive(false); 
        }
    }
}
