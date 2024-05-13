using System;
using UnityEngine;

public class TopDownRotation : MonoBehaviour{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;

    [SerializeField] private SpriteRenderer characterRenderer;
    
    private TopDownController controller;

    private void Awake(){
        controller = GetComponent<TopDownController>();

    }

    private void Start(){
        controller.OnLookEvent += OnAim;
    }
    private void OnAim(Vector2 direction){
        RotateArm(direction);
    }

    private void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;     
        
        characterRenderer.flipX = Mathf.Abs(rotZ)>90f;

        armRenderer.flipY = characterRenderer.flipX;
        armPivot.rotation = Quaternion.Euler(0,0,rotZ);
    }
}
