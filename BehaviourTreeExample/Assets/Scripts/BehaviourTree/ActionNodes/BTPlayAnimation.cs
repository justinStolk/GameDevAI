using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlayAnimation : BTNode
{
    private Animator animator;
    private string animationName;
    public BTPlayAnimation(Animator _animator, string _animationName)
    {
        animator = _animator;
        animationName = _animationName;
    }
    public override BTResult Run()
    {

        animator.Play(animationName, -1, 0.5f);
        //animator.SetTrigger(animationName);
        return BTResult.Success;
    }
}
