using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    protected Animator animator;
    protected TopDownController controller;

    protected virtual void Awake(){
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<TopDownController>();
    }
}
