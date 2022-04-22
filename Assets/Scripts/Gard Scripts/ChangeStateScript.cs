using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStateScript : MonoBehaviour
{

    public FreezePower _FreezePower;

    public SpriteRenderer _SpriteRenderer;
    
    void Update()
    {
        UpdateState();
    }


    public void ChangeState()
    {
        print("changing state");
        
        if (gameObject.tag == "Water")
        {
            gameObject.tag = "Ice";
        }
        else if (gameObject.tag == "Ice")
        {
            gameObject.tag = "Water";
        }
    }
    
    private void UpdateState()
    {
        if (gameObject.tag == "Water")
        {
            _SpriteRenderer.color = Color.blue;
        }

        if (gameObject.tag == "Ice")
        {
            _SpriteRenderer.color = Color.cyan;
        }


    }
}
