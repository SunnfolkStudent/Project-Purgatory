using System;
using UnityEngine;

public class LightPowerSpot : MonoBehaviour
{
    [SerializeField] private Input _input;
    public Camera Cam;
    public LineRenderer LineRenderer;
    public Transform lightPoint;

    private void Start()
    {
        DisableLight();
    }

    private void Update()
    {
        if (_input.Light)
        {
            EnableLight();
        }

        if (_input.Light)
        {
            UpdateLight();
        }

        if (!_input.Light)
        {
            DisableLight();
        }
    }

    private void EnableLight()
    {
        LineRenderer.enabled = true;
    }

    private void UpdateLight()
    {
        
    }

    private void DisableLight()
    {
        LineRenderer.enabled = false;
    }
}
