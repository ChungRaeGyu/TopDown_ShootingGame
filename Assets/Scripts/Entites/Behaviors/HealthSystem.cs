using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;

    private CharacterStatsHandler statHandler;
    private float timeSinceLastChange = float.MaxValue;//공격받고 시간이 얼마나 지났는지
    private bool isAttacked = false;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public float CurrentHealth{get;private set;}

    public float MaxHealth =>statHandler.CurrentStat.maxHealth;
    
    private void Awake(){
        statHandler = GetComponent<CharacterStatsHandler>();
    }
    private void Start(){
        CurrentHealth = MaxHealth;
    }

    private void Update(){
        if(isAttacked && timeSinceLastChange <healthChangeDelay){
            timeSinceLastChange += Time.deltaTime;
            if(timeSinceLastChange >= healthChangeDelay){
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(float change){
        if(timeSinceLastChange < healthChangeDelay){
            return false;
        }


        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = Mathf.Clamp(CurrentHealth,0,MaxHealth);

        if(CurrentHealth<=0f){
            CallDeath();
            return true;
        }
        if(change>=0){
            OnHeal?.Invoke();
        }else{
            OnDamage?.Invoke();
            isAttacked = true;
        }
        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}
