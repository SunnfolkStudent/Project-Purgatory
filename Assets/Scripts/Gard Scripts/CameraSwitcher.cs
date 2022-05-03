using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject Camera3;
    public GameObject Camera4;
    public GameObject FollowCamera;
    
    
    void Start()
    {
        FollowCamera.SetActive(true);
        Camera1.SetActive(false);
        Camera2.SetActive(false);
        Camera3.SetActive(false);
        Camera4.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Camera1")
        {
            FollowCamera.SetActive(false);
            Camera1.SetActive(true);
        }

        if (other.gameObject.tag == "Camera2")
        {
            FollowCamera.SetActive(false);
            Camera2.SetActive(true);
        }

        if (other.gameObject.tag == "Camera3")
        {
            FollowCamera.SetActive(false);
            Camera3.SetActive(true);
        }

        if (other.gameObject.tag == "Camera4")
        {
            FollowCamera.SetActive(false);
            Camera4.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Camera1Exit")
        {
            Camera1.SetActive(false);
            FollowCamera.SetActive(true);
        }

        if (other.gameObject.tag == "Camera2Exit")
        {
            Camera2.SetActive(false);
            FollowCamera.SetActive(true);
        }

        if (other.gameObject.tag == "Camera3Exit")
        {
            Camera3.SetActive(false);
            FollowCamera.SetActive(true);
        }

        if (other.gameObject.tag == "Camera4Exit")
        {
            Camera4.SetActive(false);
            FollowCamera.SetActive(true);
        }
    }
}
