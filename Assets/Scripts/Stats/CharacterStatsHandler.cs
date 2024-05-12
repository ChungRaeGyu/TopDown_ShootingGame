using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour {
    //기본스탯

    [SerializeField] private CharacterStat baseStat;

    public CharacterStat CurrentStat {get; private set;} //캐릭터의 스탯을 불러오기 위해
    public List<CharacterStat> statModifiers = new List<CharacterStat>(); //이거는 어따 쓰는 거임?

    private void Awake(){
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        AttackSO attackSO = null;
        if(baseStat.attackSO!= null){
            attackSO = Instantiate(baseStat.attackSO);
        }

        CurrentStat = new CharacterStat{attackSO = attackSO};

        CurrentStat.statsChangeType = baseStat.statsChangeType;
        CurrentStat.maxHealth = baseStat.maxHealth;
        CurrentStat.speed = baseStat.speed;
    }
}