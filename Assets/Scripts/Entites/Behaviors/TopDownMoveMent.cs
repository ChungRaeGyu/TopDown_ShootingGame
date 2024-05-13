using System;
using UnityEngine;

public class TopDownMoveMent :MonoBehaviour{
    //실제로 이동이 일어날 컴포넌트

    private TopDownController controller;
    private Rigidbody2D movementRigidbody;
    private CharacterStatsHandler characterStatsHandler;

    private Vector2 movementDirection = Vector2.zero;
    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0f;

    private void Awake(){
        //
        controller = GetComponent<TopDownController> ();
        movementRigidbody = GetComponent<Rigidbody2D>();
        characterStatsHandler = GetComponent<CharacterStatsHandler>();
    }
    private void Start(){
        controller.OnMoveEvent += Move;
    }
    private void Move(Vector2 direction)
    {

        movementDirection = direction;
    }
    private void FixedUpdate(){
        //여기는 물리 업데이트 관련
        //rigidbody는 물리
        ApplyMovement(movementDirection);

        if(knockbackDuration > 0f){
            knockbackDuration -=Time.fixedDeltaTime; 
        }
    }

    public void ApplyKnockback(Transform Other, float power, float duration){
        knockbackDuration = duration;
        knockback = -(Other.position - transform.position).normalized * power;
    }
    private void ApplyMovement(Vector2 direction){
        direction = direction * characterStatsHandler.CurrentStat.speed;

        if(knockbackDuration >0.0f){
            direction += knockback;
        }
        movementRigidbody.velocity = direction;
    }
}