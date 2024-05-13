using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class TopDownShooting : MonoBehaviour{
    private TopDownController controller;

    public GameObject testPrefab;

    public GameObject WeaponPivot;
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

    private void OnShoot(AttackSO attackSO)
    {
        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO; //형변환
        if(rangedAttackSO == null) return;
        float projectilesAngleSpace = rangedAttackSO.multipleProjectilesAngle;
        int numberOfProjectilesPerShot = rangedAttackSO.numberofProgjectilesPerShot;
        
        float minAngle = -(numberOfProjectilesPerShot / 2f)*projectilesAngleSpace * 0.5f * rangedAttackSO.multipleProjectilesAngle;
        for(int i=0; i<numberOfProjectilesPerShot; i++){
            float angle = minAngle +i *projectilesAngleSpace;
            float randomSpread = Random.Range(-rangedAttackSO.spread, rangedAttackSO.spread);
            angle+=randomSpread;
            CreateProjectile(rangedAttackSO,angle); // Projectile = 투사체

        }
    }

    private void CreateProjectile(RangedAttackSO rangedAttackSO,float angle)
    {
        GameObject obj = Instantiate(testPrefab);
        obj.transform.position = projectileSpawnPosition.position;
        ProjectileController attackController = obj.GetComponent<ProjectileController>();
        attackController.InitializeAttack(RotateVector2(aimDirection,angle),rangedAttackSO);
        Instantiate(testPrefab, projectileSpawnPosition.position, WeaponPivot.transform.rotation);
    }
    private static Vector2 RotateVector2(Vector2 v, float angle){
        return Quaternion.Euler(0f,0f,angle)* v;
    }
}
