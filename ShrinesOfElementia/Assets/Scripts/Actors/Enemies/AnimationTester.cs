//Author: Joakim Ljung

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTester : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger("Death");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetTrigger("Idle");
        }
    }
}
