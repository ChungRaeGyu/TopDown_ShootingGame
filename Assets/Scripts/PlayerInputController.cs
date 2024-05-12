
//player input controller 진행 이건 TopDonwController를 상속받는 클래스
using UnityEngine;
using UnityEngine.InputSystem;

/*동작순서 : 
    1.PlayerInputController스크립트 OnMove에서 InputActions에서 키 값을 받는다.
    2.OnMove에서 TopDownController스크립트에 있는 CallMoveEvent를 호출한다.
    3.CallMoveEvent에서 Invoke로 InputActions의 OnMoveMentEvent를 호출한다.
    4.InputSystem에서 OnMoveMentEvent로 받은 값을 
        PlayerInput컴포넌트가 SendMessage를 통해 오브젝트의 모든 컴포넌트에 뿌려준다.
    5.SendMessage가 뿌려준 값을 TopDownMoveMent가 받아서 저장한다.
    6.TopDownMoveMent에 있는 FixedUpdate에서 움직임이 실행된다.
*/
public class PlayerInputController : TopDownController
{
    private Camera _camera;

    private void Awake() {
        _camera = Camera.main;
    }
    //여기서 방향값을 정하는 전 처리 작업을 하고
    public void OnMove(InputValue value){ //InputValue InputActions에서 받은 키의 값
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
