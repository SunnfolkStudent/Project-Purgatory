using UnityEngine;
using Input = Assets.Scripts.General_Scripts.Input;

[RequireComponent(typeof(AudioSource))]
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

    #region Animation and sounds
    
    private Animator anim;
    public bool isJumping;

    private AudioSource source;
    [SerializeField] private AudioClip walking; 

    #endregion

   
    private void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        _Input = GetComponent<Input>();
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        isFacingLeft = false;
        isFacingRight = true;
    }

    private void Update()
    {
        if (_Input.Jump && IsGrounded()/*grounded*/)
        {
            _Rigidbody2D.velocity = new Vector2(_Rigidbody2D.velocity.x, jumpSpeed);
            isJumping = true;
        }
        
        FlipPlayer();
    }

    private void FixedUpdate()
    {
        _Rigidbody2D.velocity = new Vector2(_Input.MoveVector.x * moveSpeed, _Rigidbody2D.velocity.y);

        if (isSliding)
        {
           //Decelerate(); This is disabled as its too buggy in the current implimentation
        }
    }

    private void FlipPlayer()
    {
        if (_Input.MoveVector.x != 0)
        {
            var scale = transform.localScale;
            transform.localScale = new Vector3(_Input.MoveVector.x, scale.y, scale.z);
        }
        animations();
    }

    private void animations()
    {
        if (IsGrounded())
        {
            //          if this       is not 0  True     false
            anim.Play(_Input.MoveVector.x != 0 ? "Walk" : "Idle");
            /*if (_Input.MoveVector.x != 0)
            {
                anim.Play("Walk");
            }
            else 
            {
                anim.Play("Idle");
            }*/
        }
        else
        {
            anim.Play(_Rigidbody2D.velocity.y > 0 ? "Jump" : "Falling");
            
           /* if (_Rigidbody2D.velocity.y >= 0.1)
            {
                anim.Play("Jump");
            }
            else if (_Rigidbody2D.velocity.y <= -0.1)
            {
                anim.Play("Falling");
            }*/
        }
    }

    public void PlayWalkSound()
    {
        source.pitch = Random.Range(0.5f, 1.5f);
        source.PlayOneShot(walking);
    }
    
    private bool IsGrounded()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, raycastRange, GroundLayer);

        if (hit.transform == null) return false;
        
        if (hit.transform.CompareTag("Ground"))
        {
            isSliding = false;
            return true;
        }
        else if (hit.transform.CompareTag("Box"))
        {
            isSliding = false;
            return true;
        }
        else if (hit.transform.CompareTag("Ice"))
        {
            isSliding = true;
            return true;
        }
        else
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
}