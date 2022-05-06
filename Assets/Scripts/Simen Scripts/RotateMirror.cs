using UnityEngine;
using Input = Assets.Scripts.General_Scripts.Input;
[RequireComponent(typeof(AudioSource))]
public class RotateMirror : MonoBehaviour
{
    [SerializeField] private Input _input;
    [SerializeField] private GameObject[] mirror;
    [SerializeField] private bool canFlip;
    private Animator _animator;
    [SerializeField] private AudioClip leverSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (_input.Interact && canFlip)
        {
            audioSource.PlayOneShot(leverSound);
            RotateArrayObject(mirror);
        }
    }

    private void RotateArrayObject(GameObject[] mirrors)
    {
        
        foreach (var mirror in mirrors)
        {
            mirror.transform.Rotate(new Vector3(0, 0, -90));
            _animator.Play("LightLever");
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