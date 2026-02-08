using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Level2 : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.Play("FadeIn");
    }

}
