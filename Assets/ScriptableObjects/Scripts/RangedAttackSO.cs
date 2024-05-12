using UnityEngine;
[CreateAssetMenu(fileName = "RangedAttackSO",menuName = "TopDownController/Attacks/Range", order = 1)] //order는 칸이겠지?
public class RangedAttackSO : AttackSO{
    [Header("Ranged Attack Info")]
    public string bulletNameTag;
    public float duration;
    public float spread;
    public int numberofProgjectilesPerShot;
    public float multipleProjectilesAngle;
    public Color projectileColor;
}