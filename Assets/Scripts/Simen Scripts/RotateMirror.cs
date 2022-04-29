using UnityEngine;
using Input = Assets.Scripts.General_Scripts.Input;

public class RotateMirror : MonoBehaviour
{
    [SerializeField] private Input _input;
    [SerializeField] private GameObject[] mirror;
    [SerializeField] private bool canFlip;
    
    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (_input.Flip && canFlip)
        {
            RotateArrayObject(mirror);
        }
    }

    private void RotateArrayObject(GameObject[] mirrors)
    {
        
        foreach (var mirror in mirrors)
        {
            mirror.transform.Rotate(new Vector3(0, 0, -90));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canFlip = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canFlip = false;
        }
    }
}