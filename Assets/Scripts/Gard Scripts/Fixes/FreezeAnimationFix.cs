using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeAnimationFix : MonoBehaviour
{

    public FreezePower _FreezePower;

    [SerializeField] private Animator anim;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (_FreezePower.PlayFreezeAnimation)
        {
            anim.Play("Freeze");
            _FreezePower.PlayFreezeAnimation = false;
        }
    }
}
