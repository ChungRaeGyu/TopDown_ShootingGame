using System;
using UnityEngine;

public class TopDownContactEnemyController : TopDownEnemyController{
    [SerializeField][Range(0f,100f)] private float followRange;
    [SerializeField]private string targetTag = "Player";
    private bool isCollidingWithTarget;

    [SerializeField] private SpriteRenderer characterRenderer;

    private HealthSystem healthSystem;
    private HealthSystem collidingTargetHealthSystem;
    private TopDownMoveMent collidingMovement;

    protected override void Start(){
        base.Start();

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamage += OnDamage;
    }

    private void OnDamage()
    {
        followRange = 100f;
    }

    protected override void FixedUpdate(){
        base.FixedUpdate();

        if(isCollidingWithTarget){
            ApplyHealthChange();
        }
        Vector2 direction = Vector2.zero;
        if(DistanceToTarget() < followRange){
            direction = DirectionToTarget();
        }
        CallMoveEvent(direction);
        Rotate(direction);
    }

    private void ApplyHealthChange()
    {
        AttackSO attackSO = stats.CurrentStat.attackSO;
        bool isAttackable = collidingTargetHealthSystem.ChangeHealth(-attackSO.power);

        if(isAttackable&&attackSO.isOneKnockBack){
            if(collidingMovement!=null){
                collidingMovement.ApplyKnockback(transform,attackSO.KnockbackPower,attackSO.knockbackTime);
            }
        }
    }


    private void Rotate(Vector2 direction){
        float rotZ = Mathf.Atan2(direction.y,direction.x)* Mathf.Rad2Deg;
        characterRenderer.flipX = Mathf.Abs(rotZ)>90f;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        GameObject receiver = collision.gameObject;

        if(!receiver.CompareTag(targetTag)){
            return;
        }

        collidingTargetHealthSystem = collision.GetComponent<HealthSystem>();
        if(collidingTargetHealthSystem !=null){
            isCollidingWithTarget = true;
        }
        collidingMovement = collision.GetComponent<TopDownMoveMent>(); 
    } 

    private void OnTriggerExit2D(Collider2D collision) {
        if(!collision.CompareTag(targetTag)){return;}
        isCollidingWithTarget = false;
    }
}
