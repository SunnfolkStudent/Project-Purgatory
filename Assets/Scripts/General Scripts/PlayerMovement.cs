using System;
using UnityEngine;
using Input = Assets.Scripts.General_Scripts.Input;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    public Input _Input;
    public Rigidbody2D _Rigidbody2D;
    public FreezePower _FreezePower;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;

    [SerializeField] private float raycastRange;
    public LayerMask interActLayer; // You set these in the inspector. Multiply can be used at once.

    private bool isFacingLeft;
    private bool isFacingRight;

    private bool isSliding;
    private bool inAir;

    private float deceleration;
    private bool isMovingLeft;
    private bool isMovingRight;
    private bool stoppedLeft;
    private bool stoppedRight;
    [SerializeField] private float decelerationForce = 200f;
    [SerializeField] private float decelerationAmount = 10f;

    #region Animation and sounds
    
    private Animator anim;
    public bool isJumping;

    private AudioSource source;
    [SerializeField] private AudioClip walking;
    [SerializeField] private AudioClip jumping;

    public bool playingFreezeAnimation;
    [HideInInspector] public bool CantPlayStreamSound;

    public bool playingAquireFreezeAnimation;
    private bool canPlayAquireFreezeAnimation;
    public bool HideFreezePowerUp;

    public bool playingAquireLightAnimation;
    private bool canPlayAquireLightAnimation;
    public bool HideLightPowerup; //I WAS HERE. WAS JUST ABOUT TO IMPLEMENT THIS

    public bool playingAquireFinalSoulAnimation;
    private bool canPlayAquireFinalSoul;
    public bool HideFinalSoul;
    public bool LightUpBackground;

    public bool FreezeMovement;
    
    #endregion

    [SerializeField] private GameObject spawnPointLeft;
    [SerializeField] private GameObject RaycastTargetCenter = null; 
    [SerializeField] private GameObject RaycastTargetBehind = null;

    
    
    
    private void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        _Input = GetComponent<Input>();
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        isFacingLeft = false;
        isFacingRight = true;
        playingFreezeAnimation = true;
        canPlayAquireFreezeAnimation = true;
        canPlayAquireLightAnimation = true;
        canPlayAquireFinalSoul = true;
        FreezeMovement = false;
    }

    private void Update()
    {
        if (_Input.Jump && IsGrounded()/*grounded*/ && !FreezeMovement)
        {
            source.PlayOneShot(jumping);
            _Rigidbody2D.velocity = new Vector2(_Rigidbody2D.velocity.x, jumpSpeed);
            isJumping = true;
        }
        
        FlipPlayer();
        FreezeAnimationTrigger();
        LandingAudio();


        if (LightUpBackground)
        {
            print("light up background");
        }
        
        
    }

    

    private void FixedUpdate()
    {
        if (!FreezeMovement)
        {
            _Rigidbody2D.velocity = new Vector2(_Input.MoveVector.x * moveSpeed, _Rigidbody2D.velocity.y);
        }
       

        if (isSliding)
        {
           //Decelerate(); This is disabled as its too buggy in the current implimentation
        }
    }
    
    private void LandingAudio()
    {
        if (!IsGrounded())
        { inAir = true; }
        
        if (inAir && IsGrounded())
        {
            source.PlayOneShot(walking);
            inAir = false;
        }
    }

    private void FlipPlayer()
    {
        if (_Input.MoveVector.x != 0 && !FreezeMovement)
        {
            var scale = transform.localScale;
            transform.localScale = new Vector3(_Input.MoveVector.x, scale.y, scale.z);
        }
        animations();
    }

    private void animations()
    {
        if (IsGrounded() && !playingFreezeAnimation && !playingAquireFreezeAnimation && !playingAquireLightAnimation && !playingAquireFinalSoulAnimation && !FreezeMovement)
        {
            //          if this       is not 0  True     false
            anim.Play(_Input.MoveVector.x != 0 ? "Walk" : "Idle");
        }
        else if (!playingFreezeAnimation && !playingAquireFreezeAnimation && !playingAquireLightAnimation && !playingAquireFinalSoulAnimation && !FreezeMovement)
        {
            anim.Play(_Rigidbody2D.velocity.y > 0 ? "Jump" : "Falling");
        }

        if (GameStatus.HasPickedUpFreezePower && canPlayAquireFreezeAnimation)
        {
            anim.Play("Aquire Freeze");
            playingFreezeAnimation = true;
            HideFreezePowerUp = true;
            canPlayAquireFreezeAnimation = false;
        }

        if (GameStatus.HasPickedUpLightPower && canPlayAquireLightAnimation)
        {
            anim.Play("Aquire Light");
            playingAquireLightAnimation = true;
            HideLightPowerup = true;
            canPlayAquireLightAnimation = false;
        }

        if (GameStatus.CanPickUpFinalSoul && canPlayAquireFinalSoul)
        {
            FreezeMovement = true;
            anim.Play("AquireFinalSoul");
            playingAquireFinalSoulAnimation = true;
            HideFinalSoul = true;
            canPlayAquireFinalSoul = false;
        }
        
    }

    private void FreezeAnimationTrigger()
    {
        if (_FreezePower.PlayFreezeAnimation)
        {
            anim.Play("Freeze");
            _FreezePower.PlayFreezeAnimation = false;
        }
    }
    public void PlayWalkSound()
    {
        source.pitch = Random.Range(0.75f, 1.25f);
        source.PlayOneShot(walking);
    }
    
    private bool IsGrounded()
    {
        var position = spawnPointLeft.transform.position;

        var hit = Physics2D.Raycast(transform.position, Vector2.down, raycastRange, interActLayer);
        var hitLeft = Physics2D.Raycast(position, Vector2.down, raycastRange, interActLayer);

        if (hit) {RaycastTargetCenter = hit.transform.gameObject;}
        if (hitLeft) {RaycastTargetBehind = hitLeft.transform.gameObject;}

        if (!hit) {RaycastTargetCenter = null;}
        if (!hitLeft) {RaycastTargetBehind = null;}
        
        if (RaycastTargetCenter != null && RaycastTargetCenter.CompareTag("Ground"))
        {
            isSliding = false;
            return true;
        }
        else if (RaycastTargetBehind != null && RaycastTargetBehind.CompareTag("Ground"))
        {
            isSliding = false;
            return true;
        }
        else if (RaycastTargetCenter != null && (RaycastTargetCenter.transform.CompareTag("Box") || RaycastTargetBehind.transform.CompareTag("Box")))
        {
            isSliding = false;
            return true;
        }
        else if (RaycastTargetBehind != null && RaycastTargetCenter != null && (RaycastTargetCenter.transform.CompareTag("Ice") || RaycastTargetBehind.transform.CompareTag("Ice")))
        {
            isSliding = true;
            return true;
        }
        else if (RaycastTargetBehind != null && RaycastTargetCenter != null && RaycastTargetCenter.transform == null && RaycastTargetBehind.transform == null) return false;
        {
            return false;
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Camera1Exit" || other.gameObject.tag == "Camera2Exit" || other.gameObject.tag == "Camera3Exit" || other.gameObject.tag == "Camera4Exit")
        {
            CantPlayStreamSound = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Camera1Exit" || other.gameObject.tag == "Camera2Exit" || other.gameObject.tag == "Camera3Exit" || other.gameObject.tag == "Camera4Exit")
        {
            CantPlayStreamSound = false;
        }
    }
}