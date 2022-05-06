using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    public SceneController _SceneController;

    //[SerializeField] private GameStatus _GameStatus;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TPtoZoneA")
        {
            _SceneController.LoadScene("Zone A");
        }

        if (other.gameObject.tag == "TPtoZoneB")
        {
            _SceneController.LoadScene("Zone B");
        }

        if (other.gameObject.tag == "TPtoHUBfromZoneA")
        {
           _SceneController.LoadScene("Hub");
           GameStatus.HasCompletedZoneA = true;
        }
        
        if (other.gameObject.tag == "TPtoHUBformZoneB")
        {
            _SceneController.LoadScene("Hub");
            GameStatus.HasCompletedZoneB = true;
        }
    }
}
