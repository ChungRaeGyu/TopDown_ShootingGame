using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]private float speed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    private void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector2 direction = new Vector2(vertical, horizontal);  

        //가로세로가 1일때 대각선으로 가게 되면 루트2가 되기 때문에
        //정규화를 통해서 속도를 동일하게 맞춰준다.
        direction = direction.normalized;//정규화
        //정규화 : 길이를 1로만드는 작업

        rb.velocity = direction*speed;
    }
}
