using UnityEngine;

public class TurnAnimationsBackOn : MonoBehaviour
{
    [SerializeField] private SCRUB anim;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.canAnimate = true;
        }
    }
}