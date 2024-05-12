using System;
using UnityEngine;

public class TopDownMoveMent :MonoBehaviour{
    //실제로 이동이 일어날 컴포넌트

    private TopDonwController controller;
    private Rigidbody2D movementRigidbody;

    private Vector2 movementDirection = Vector2.zero;

    private void Awake(){
        //
        controller = GetComponent<TopDonwController> ();
        movementRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start(){
        controller.OnMoveEnvent += Move;
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
        print("실행");
        print(""+direction);
        direction = direction * 5;
        movementRigidbody.velocity = direction;
    }
}