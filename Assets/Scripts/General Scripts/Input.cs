using UnityEngine;

namespace Assets.Scripts.General_Scripts
{
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
        [HideInInspector] public bool Flip;

        void Update()
        {
            MoveVector = _Input.Player.Move.ReadValue<Vector2>();
            Jump = _Input.Player.Jump.triggered;
            Magic = _Input.Player.Magic.triggered;
            Light = _Input.Player.Light.triggered;
            Flip = _Input.Player.FlipMirror.triggered;
        }
    }
}
