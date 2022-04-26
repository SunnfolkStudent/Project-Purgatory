using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Input _Input;
    public Rigidbody2D _Rigidbody2D;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;

    [HideInInspector] public bool grounded;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask Icelayer;
    [SerializeField] private float raycastRange;

    private bool isFacingLeft;
    private bool isFacingRight;

    private bool isSliding;

    private float deceleration;
    private bool isMovingLeft;
    private bool isMovingRight;
    private bool stoppedLeft;
    private bool stoppedRight;
    [SerializeField] private float decelerationForce = 200f;
    [SerializeField] private float decelerationAmount = 10f;

    private void Start()
    {
        _Input = GetComponent<Input>();
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        isFacingLeft = false;
        isFacingRight = true;
    }

    private void Update()
    {
        if (_Input.Jump && grounded)
        {
            _Rigidbody2D.velocity = new Vector2(_Rigidbody2D.velocity.x, jumpSpeed);
        }

        FlipPlayer();
    }

    private void FixedUpdate()
    {
        
        _Rigidbody2D.velocity = new Vector2(_Input.MoveVector.x * moveSpeed, _Rigidbody2D.velocity.y);
        
        GroundCheck();
        
        if (isSliding)
        {
           Decelerate(); 
        }
        
    }

    private void FlipPlayer()
    {
        if (Keyboard.current.dKey.wasPressedThisFrame && isFacingLeft)
        {
            transform.Rotate(new Vector3(0, 180));
            isFacingRight = true;
            isFacingLeft = false;
        }

        if (Keyboard.current.aKey.wasPressedThisFrame && isFacingRight)
        {
            transform.Rotate(new Vector3(0, 180));
            isFacingLeft = true;
            isFacingRight = false;
        }
    }

    private void GroundCheck()
    {
        grounded = false;

        var hit = Physics2D.Raycast(transform.position, Vector2.down, raycastRange, GroundLayer);

        if (hit.collider != null)
        {
            grounded = true;
            isSliding = false;
        }

        var hitice = Physics2D.Raycast(transform.position, Vector2.down, raycastRange, Icelayer);

        if (hitice.collider != null)
        {
            grounded = true;
            isSliding = true;
        }
    }
    
    private void Decelerate()
    {
        if (_Input.MoveVector.x == -1)
        {
            isMovingRight = true;
            stoppedRight = false;
            stoppedLeft = false;
            deceleration = decelerationForce;
            if (_Input.MoveVector.x > -1)
            {
                moveSpeed = 1;
            }
        }
        
        if (_Input.MoveVector.x == 1)
        {
            isMovingLeft = true;
            stoppedRight = false;
            stoppedLeft = false;
            deceleration = decelerationForce;
            if (_Input.MoveVector.x < 1)
            {
                moveSpeed = 1;
            }
        }

        if (_Input.MoveVector.x == 0)
        {
            if (isMovingRight)
            {
                _Rigidbody2D.AddForce(Vector2.left * deceleration);
                stoppedRight = true;
                isMovingRight = false;
                
            }

            if (isMovingLeft)
            {
                _Rigidbody2D.AddForce(Vector2.right * deceleration);
                stoppedLeft = true;
                isMovingLeft = false;
            }
        }

        if (stoppedLeft)
        {
            _Rigidbody2D.AddForce(Vector2.right * deceleration);
            deceleration -= Time.deltaTime * deceleration * decelerationAmount;

            if (deceleration <= 1)
            {
                stoppedLeft = false;
                stoppedRight = false;
                deceleration = decelerationForce;
            }
        }
        
        
        if (stoppedRight)
        {
            _Rigidbody2D.AddForce(Vector2.left * deceleration);
            deceleration -= Time.deltaTime * deceleration * decelerationAmount;

            if (deceleration <= 1)
            {
                stoppedRight = false;
                stoppedLeft = false;
                deceleration = decelerationForce;
            }
        }
        
        
    }
    
}