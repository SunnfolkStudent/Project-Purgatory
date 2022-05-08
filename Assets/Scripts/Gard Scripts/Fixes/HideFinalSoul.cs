using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFinalSoul : MonoBehaviour
{
    [SerializeField] private PlayerMovement _PlayerMovement;
    [SerializeField] private GameObject BackgroundGlow;
    public SceneController _SceneController;

    private float startTimer = 3f;
    private float currentTimer;

    private void Start()
    {
        BackgroundGlow.SetActive(false);
        currentTimer = startTimer;
    }

    void Update()
    {

        if (_PlayerMovement.HideFinalSoul)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //gameObject.SetActive(false); 
        }

        if (_PlayerMovement.LightUpBackground)
        {
            print("lightup");
            BackgroundGlow.SetActive(true);
            currentTimer -= Time.deltaTime;
        }

        if (currentTimer <= 0)
        {
            _SceneController.LoadScene("StartUIScreen");
            print("the end");
        }
    }
}
