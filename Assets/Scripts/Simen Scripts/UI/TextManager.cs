using System;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] private GameObject Dialouge;

    private void Start()
    {
        Dialouge.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Dialouge.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Dialouge.SetActive(false);
        }
    }
}
