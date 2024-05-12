using System;
using UnityEngine;

public class TopDownShooting : MonoBehaviour{
    private TopDownController controller;

    public GameObject testPrefab;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;
    private void Awake(){
        controller = GetComponent<TopDownController>(); 
    }
    private void Start(){
        controller.OnAttackEvent += OnShoot;
        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        aimDirection = direction;
    }

    private void OnShoot()
    {
        CreateProjectile(); // Projectile = 투사체
    }

    private void CreateProjectile()
    {
        
        Instantiate(testPrefab, projectileSpawnPosition.position, Quaternion.identity);
    }
}

