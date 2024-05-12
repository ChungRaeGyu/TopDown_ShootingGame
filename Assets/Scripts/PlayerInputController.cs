
//player input controller 진행 이건 TopDonwController를 상속받는 클래스
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDonwController
{
    private Camera _camera;

    private void Awake() {
        _camera = Camera.main;
    }
    //여기서 방향값을 정하는 전 처리 작업을 하고
    public void OnMove(InputValue value){ //InputValue InputActions에서 받은 키의 값
        print("OnMove");
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput); //호출
        //실제 움직이는 처리는 여기서 하는게 아니라 PlayerMovement에서 함
    }

    public void OnLook(InputValue value){
        //normalize를 안하는 이유: 마우스의 위치가 캐릭터의 왼쪽인지 오른쪽인지만 판단할꺼기 때문에
        Vector2 newAim = value.Get<Vector2>(); 
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        CallLookEvent(newAim);
    }
}
