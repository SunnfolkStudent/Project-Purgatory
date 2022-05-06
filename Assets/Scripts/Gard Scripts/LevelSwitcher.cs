using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    public SceneController _SceneController;

    [SerializeField] private GameObject LoadingScreen;

    //[SerializeField] private GameStatus _GameStatus;

    private void Start()
    {
        LoadingScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TPtoZoneA")
        {
            LoadingScreen.SetActive(true);
            _SceneController.LoadScene("Zone A");
        }

        if (other.gameObject.tag == "TPtoZoneB")
        {
            LoadingScreen.SetActive(true);
            _SceneController.LoadScene("Zone B");
        }

        if (other.gameObject.tag == "TPtoHUBfromZoneA")
        { 
            LoadingScreen.SetActive(true); 
            GameStatus.HasCompletedZoneA = true;
           _SceneController.LoadScene("Hub");
           
        }
        
        if (other.gameObject.tag == "TPtoHUBformZoneB")
        {
            LoadingScreen.SetActive(true);
            GameStatus.HasCompletedZoneB = true;
            _SceneController.LoadScene("Hub");
        }

        if (other.gameObject.tag == "TPtoZoneC")
        {
            LoadingScreen.SetActive(true);
            _SceneController.LoadScene("Zone C");
        }
    }
}
