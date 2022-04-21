using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input : MonoBehaviour
{
   
    private PlayerInputs _Input;
        
    private void Awake()
    {
        _Input = new PlayerInputs();
    }
    private void OnEnable()
    {
        _Input.Enable();
    }
    private void OnDisable()
    {
        _Input.Disable();
    }
    
    [HideInInspector] public Vector2 MoveVector;
    [HideInInspector] public bool Jump;
    [HideInInspector] public bool Magic;
    [HideInInspector] public bool Light;

    void Update()
    {
        MoveVector = _Input.Player.Move.ReadValue<Vector2>();
        Jump = _Input.Player.Jump.triggered;
        Magic = _Input.Player.Magic.triggered;
        Light = _Input.Player.Light.triggered;
    }
}
