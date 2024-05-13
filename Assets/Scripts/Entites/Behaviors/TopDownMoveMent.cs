using System;
using UnityEngine;

public class TopDownMoveMent :MonoBehaviour{
    //실제로 이동이 일어날 컴포넌트

    private TopDownController controller;
    private Rigidbody2D movementRigidbody;
    private CharacterStatsHandler characterStatsHandler;

    private Vector2 movementDirection = Vector2.zero;

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
    }
    private void ApplyMovement(Vector2 direction){
        direction = direction * characterStatsHandler.CurrentStat.speed;
        movementRigidbody.velocity = direction;
    }
}