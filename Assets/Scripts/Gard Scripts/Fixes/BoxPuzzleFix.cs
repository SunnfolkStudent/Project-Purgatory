using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPuzzleFix : MonoBehaviour
{
    
    private CompositeCollider2D _CompositeCollider2D;

    private bool hasentered;
    
    void Start()
    {
        _CompositeCollider2D = gameObject.GetComponent<CompositeCollider2D>();
    }

    
    void Update()
    {
        if (_CompositeCollider2D.isTrigger = false)
        {
            print("cant pass through");
        }

        if (hasentered)
        {
            _CompositeCollider2D.isTrigger = false;
            print("solid");
        }
        else
        {
            _CompositeCollider2D.isTrigger = true;
            print("passthrough");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BoxPuzzleFix")
        {
            hasentered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "BoxPuzzleFix")
        {
            hasentered = false;
        }
    }
}
