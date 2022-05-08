using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCcameraSwitcher : MonoBehaviour
{
    public GameObject PuzzleCamera;
    public GameObject FollowCamera;

    private void Start()
    {
        FollowCamera.SetActive(true);
        PuzzleCamera.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Camera1")
        {
            PuzzleCamera.SetActive(true);
            FollowCamera.SetActive(false);
        }

        if (other.gameObject.tag == "Camera1Exit")
        {
            FollowCamera.SetActive(true);
            PuzzleCamera.SetActive(false);
        }
    }
}
