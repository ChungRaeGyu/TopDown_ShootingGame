using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터와 플레이어가 같이 사용하는 인터페이스? 같은 느낌
public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEnvent; //Action은 무조건 void만 반환해야 함 아니면 Func를 사용
    public event Action<Vector2> OnLookEvent;

    //같은 컴포넌트에 있는곳에 다 뿌림
    public void CallMoveEvent(Vector2 direction){        
        OnMoveEnvent?.Invoke(direction); // ?. null값 허용(없으면 말고 있으면 실행);
    }

    public void CallLookEvent(Vector2 direction){
        OnLookEvent?.Invoke(direction); //InputActions를 사용할때 Invoke로 호출해야한다.
    }
}   
