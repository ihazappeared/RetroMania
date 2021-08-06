using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public string State;
    public bool attackBool;
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        State = "";
    }

    public void Walk(bool walk)
    {
        anim.SetBool(AnimationTags.WALK_PARAMETER, walk);
        State = "Walk";
    }

    public void Attack()
    {
        anim.SetTrigger(AnimationTags.ATTACK_TRIGGER);
        State = "Attack";
        attackBool = anim.GetBool(AnimationTags.ATTACK_TRIGGER);
    }

    public void Dead()
    {
        anim.SetTrigger(AnimationTags.DEAD_TRIGGER);
        State = "Dead";
    }
}
