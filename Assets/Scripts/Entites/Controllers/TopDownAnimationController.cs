using System;
using Unity.Mathematics;
using UnityEngine;

public class TopDownAnimationController : AnimationController{
    private static readonly int isWalking = Animator.StringToHash("isWalking");
    private static readonly int isHit = Animator.StringToHash("isHit");
    private static readonly int attack = Animator.StringToHash("attack");

    private readonly float magnituteThreshold = 0.5f;//0.5이상의 속도는 나와야 한다.
    private HealthSystem healthSystem;
    protected override void Awake()
    {
        base.Awake();
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start(){
        controller.OnAttackEvent += Attacking;
        controller.OnMoveEvent += Move;

        if(healthSystem !=null){
            healthSystem.OnDamage += Hit;
            healthSystem.OnInvincibilityEnd += InvincibilityEnd;
        }
    }
    private void Move(Vector2 vector){
        //magnitude = 벡터의 크기
        animator.SetBool(isWalking, vector.magnitude > magnituteThreshold);
    }
    private void Attacking(AttackSO sO)
    {
        animator.SetTrigger(attack);
    }
    private void Hit(){
        animator.SetBool(isHit,true);
    }
    private void InvincibilityEnd(){
        //무적
        animator.SetBool(isHit,false);
    }
}