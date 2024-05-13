using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

internal class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    private RangedAttackSO attackData;
    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private bool fxOnDestory=true;

    public void Awake(){
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }
    private void Update() {
        if(!isReady){
            return;
        }
        currentDuration += Time.deltaTime;

        if(currentDuration > attackData.duration){
            DestroyProjectile(transform.position,false);
        }

        rigidbody.velocity = direction * attackData.speed;
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if(createFx){
            //TODO : ParticleSystem에 대해서 배우고, 무기 NameTag로 해당하는 FX가져오기
        }
        gameObject.SetActive(false);
    }

    public void InitializeAttack(Vector2 direction, RangedAttackSO attackData){
        this.attackData = attackData;
        this.direction = direction;

        UpdateProjectileSprite();
        trailRenderer.Clear();
        currentDuration = 0;
        spriteRenderer.color = attackData.projectileColor;

        transform.right = this.direction;

        isReady = true;
    }

    private void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * attackData.size;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer)){
            Vector2 destoryPosition = collision.ClosestPoint(transform.position)-direction*0.2f;
            DestroyProjectile(destoryPosition, fxOnDestory);
            Debug.Log("실행");
        }
        else if(IsLayerMatched(attackData.target.value, collision.gameObject.layer)){
            print(collision.name);
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if(healthSystem!=null){
                bool isAttackApplied = healthSystem.ChangeHealth(-attackData.power);

                if(isAttackApplied && attackData.isOneKnockBack){
                    ApplyKnockback(collision);
                }
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }

    private void ApplyKnockback(Collider2D collision)
    {
        TopDownMoveMent movement= collision.GetComponent<TopDownMoveMent>();
        if(movement!=null){
            movement.ApplyKnockback(transform,attackData.KnockbackPower,attackData.knockbackTime);
        }
    }

    private bool IsLayerMatched(int value, int layer)
    {
        return value ==(value | 1 <<layer);
    }
}
