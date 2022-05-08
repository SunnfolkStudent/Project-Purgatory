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
        [HideInInspector] public bool Interact;
        [HideInInspector] public bool Reload;

        void Update()
        {
            MoveVector = _Input.Player.Move.ReadValue<Vector2>();
            Interact = _Input.Player.Interact.triggered;
            Jump = _Input.Player.Jump.triggered;
            Reload = _Input.Player.Reload.triggered;
        }
    }
}
