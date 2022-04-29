using UnityEngine;

namespace Assets.Scripts.General_Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private Input _Input;
        public Rigidbody2D _Rigidbody2D;
    
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float jumpSpeed = 10f;

        private bool grounded;
        public bool canInput;

        [SerializeField] private LayerMask GroundLayer;
        [SerializeField] private float raycastRange;
        private void Start()
        {
            _Input = GetComponent<Input>();
            _Rigidbody2D = GetComponent<Rigidbody2D>();
            canInput = true;
        }

        private void Update()
        {
            if (!canInput) return;
            transform.position += new Vector3(_Input.MoveVector.x * moveSpeed, 0f, 0f) * Time.deltaTime;

            if (_Input.Jump && grounded)
            {
                print("jumped");
                _Rigidbody2D.velocity += new Vector2(0f, jumpSpeed);
            }

            if (_Input.Magic)
            {
                print("Used magic");
            }

            if (_Input.Light)
            {
                print("You switched light");
            }
        }
    
        private void FixedUpdate()
        {
            grounded = false;

            var hit = Physics2D.Raycast(transform.position, Vector2.down, raycastRange, GroundLayer);

            if (hit.collider != null)
            {
                print("isGround");
                grounded = true;
            }
        }
    }
}
