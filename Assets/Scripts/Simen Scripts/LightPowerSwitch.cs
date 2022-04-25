using UnityEngine;

namespace Assets.Scripts.Simen_Scripts.Abilities
{
    public class LightPowerSwitch : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private bool canAnimate = true;

        private void Start()
        {
            canAnimate = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("LightBeam") && canAnimate)
            {
                print("I should play animation");
                _animator.Play("OpenDoor");
                canAnimate = false;
            }
        }
    }
}
